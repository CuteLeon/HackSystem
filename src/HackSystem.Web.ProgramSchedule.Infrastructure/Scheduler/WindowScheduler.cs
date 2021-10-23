using HackSystem.LRU;
using HackSystem.Web.Component.Configurations;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Scheduler;
using static HackSystem.Web.ProgramSchedule.Scheduler.IWindowScheduler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;

public class WindowScheduler : IWindowScheduler
{
    public event WindowScheduleHandler? OnWindowSchedule;
    private readonly ILogger<WindowScheduler> logger;
    private readonly WebComponentTierConfiguration tierConfiguration;
    private readonly LRUContainer<string, ProgramWindowDetail> windowLRUContainer;

    public WindowScheduler(
        ILogger<WindowScheduler> logger,
        IOptionsMonitor<WebComponentTierConfiguration> tierConfiguration)
    {
        this.logger = logger;
        this.tierConfiguration = tierConfiguration.CurrentValue;
        this.windowLRUContainer = new(window => window.WindowId, int.MaxValue);
    }

    public async Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        if (changeState == WindowChangeStates.Launch)
        {
            windowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.Add(windowDetail);
        }
        else if (changeState == WindowChangeStates.Active)
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
        }
        else if (changeState == WindowChangeStates.Inactive)
        {
            var headWindow = this.windowLRUContainer.HeadValue!;
            if (headWindow == windowDetail)
            {
                var previewWindow = this.windowLRUContainer.GetPreviousValue(headWindow);
                if (previewWindow is not null)
                    this.windowLRUContainer.BringToHead(previewWindow);
            }
            else
            {
                windowDetail.TierIndex = headWindow.TierIndex;
                headWindow.TierIndex = this.GetNewTierIndex();
                this.windowLRUContainer.MoveToAfter(windowDetail, this.windowLRUContainer.HeadValue!);
            }
        }
        else if (changeState == WindowChangeStates.Destory)
        {
            windowDetail.TierIndex = this.tierConfiguration.BasicProgramSubscript;
            this.windowLRUContainer.Remove(windowDetail);
        }

        this.logger.LogInformation($"Window {changeState} request handled, {windowDetail.Caption} ({windowDetail.TierIndex}).");
        this.OnWindowSchedule?.Invoke(windowDetail);
        return true;
    }

    private int GetNewTierIndex()
    {
        var newTierIndex = this.windowLRUContainer.HeadValue?.TierIndex + 1 ?? this.tierConfiguration.BasicProgramSubscript;
        if (newTierIndex >= this.tierConfiguration.BasicProgramSuperscript)
        {
            this.logger.LogInformation("Reach program superscript, resort all window tier index...");
            var tierIndex = this.tierConfiguration.BasicProgramSubscript;
            foreach (var window in this.windowLRUContainer.GetValuesFromTail())
            {
                window.TierIndex = tierIndex++;
            }
            newTierIndex = --tierIndex;
        }
        return newTierIndex;
    }
}
