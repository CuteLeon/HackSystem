using HackSystem.LRU;
using HackSystem.Web.Component.Configurations;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Scheduler;
using static HackSystem.Web.ProgramSchedule.Scheduler.IWindowScheduler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;

public class WindowScheduler : IWindowScheduler
{
    public event WindowScheduleHandler? OnWindowSchedule;
    private readonly ILogger<WindowScheduler> logger;
    private readonly IIntermediaryPublisher publisher;
    private readonly WebComponentTierConfiguration tierConfiguration;
    private readonly LRUContainer<string, ProgramWindowDetail> windowLRUContainer;

    public WindowScheduler(
        ILogger<WindowScheduler> logger,
        IIntermediaryPublisher publisher,
        IOptionsMonitor<WebComponentTierConfiguration> tierConfiguration)
    {
        this.logger = logger;
        this.publisher = publisher;
        this.tierConfiguration = tierConfiguration.CurrentValue;
        this.windowLRUContainer = new(window => window.WindowId, int.MaxValue);
    }

    public async Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        this.logger.LogInformation($"Schedule Window {changeState}, {windowDetail.Caption} ({windowDetail.TierIndex})...");
        var scheduled = false;
        switch (changeState)
        {
            case WindowChangeStates.Launch:
                scheduled = LaunchWindow(windowDetail);
                break;
            case WindowChangeStates.Active:
                scheduled = ActiveWindow(windowDetail);
                break;
            case WindowChangeStates.Inactive:
                scheduled = InactiveWindow(windowDetail);
                break;
            case WindowChangeStates.ToggleActive:
                scheduled = ToggleWindowActive(windowDetail);
                break;
            case WindowChangeStates.Destory:
                scheduled = DestoryWindow(windowDetail);
                break;
            default: break;
        }

        if (scheduled)
        {
            this.logger.LogInformation($"Window {changeState} scheduled, {windowDetail.Caption} ({windowDetail.TierIndex}).");
            await this.publisher.PublishEvent(new WindowChangeEvent(changeState, windowDetail));
            this.OnWindowSchedule?.Invoke(windowDetail);
        }
        return scheduled;
    }

    private bool DestoryWindow(ProgramWindowDetail windowDetail)
    {
        windowDetail.TierIndex = this.tierConfiguration.BasicProgramEdge;
        this.windowLRUContainer.Remove(windowDetail);
        return true;
    }

    private bool ToggleWindowActive(ProgramWindowDetail windowDetail)
    {
        if (this.windowLRUContainer.HeadValue == windowDetail &&
            windowDetail.WindowState != ProgramWindowStates.Minimized)
            return this.InactiveWindow(windowDetail);
        else
            return this.ActiveWindow(windowDetail);
    }

    private bool InactiveWindow(ProgramWindowDetail windowDetail)
    {
        var headWindow = this.windowLRUContainer.HeadValue!;
        if (headWindow == windowDetail)
        {
            var previewWindow = windowDetail;
            if (windowDetail.AllowMinimized)
                windowDetail.WindowState = ProgramWindowStates.Minimized;
            do
            {
                previewWindow = this.windowLRUContainer.GetPreviousValue(previewWindow);
            }
            while (previewWindow is not null && previewWindow.WindowState == ProgramWindowStates.Minimized);
            if (previewWindow is not null)
            {
                previewWindow.TierIndex = GetNewTierIndex();
                this.windowLRUContainer.BringToHead(previewWindow);
            }
        }
        else
        {
            windowDetail.TierIndex = headWindow.TierIndex;
            headWindow.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.MoveToAfter(windowDetail, this.windowLRUContainer.HeadValue!);
        }
        return true;
    }

    private bool ActiveWindow(ProgramWindowDetail windowDetail)
    {
        if (this.windowLRUContainer.HeadValue == windowDetail)
        {
            if (windowDetail.WindowState == ProgramWindowStates.Minimized)
                windowDetail.WindowState = windowDetail.LastWindowState;
            else
                return false;
        }
        else
        {
            if (windowDetail.WindowState == ProgramWindowStates.Minimized)
                windowDetail.WindowState = windowDetail.LastWindowState;
            windowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.BringToHead(windowDetail);
        }
        return true;
    }

    private bool LaunchWindow(ProgramWindowDetail windowDetail)
    {
        windowDetail.TierIndex = this.GetNewTierIndex();
        this.windowLRUContainer.Add(windowDetail);
        return true;
    }

    private int GetNewTierIndex()
    {
        var newTierIndex = this.windowLRUContainer.HeadValue?.TierIndex + 1 ?? this.tierConfiguration.BasicProgramEdge;
        if (newTierIndex >= this.tierConfiguration.ProgramDivider)
        {
            this.logger.LogInformation("Reach program superscript, resort all window tier index...");
            var tierIndex = this.tierConfiguration.BasicProgramEdge;
            foreach (var window in this.windowLRUContainer.GetValuesFromTail())
            {
                window.TierIndex = tierIndex++;
            }
            newTierIndex = --tierIndex;
        }
        return newTierIndex;
    }
}
