using HackSystem.LRU;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Scheduler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;

public class WindowScheduleContainer : IWindowScheduleContainer
{
    private readonly ILogger<WindowScheduleContainer> logger;
    private readonly LRUContainer<string, ProgramWindowDetail> windowLRUContainer;

    public int WindowTierIndexLowEdge { get; set; }
    public int WindowTierIndexHighEdge { get; set; }
    public ProgramWindowDetail? ActivatedWindow { get; protected set; }

    public WindowScheduleContainer(ILogger<WindowScheduleContainer> logger)
    {
        this.logger = logger;
        this.windowLRUContainer = new(window => window.WindowId, int.MaxValue);
    }

    public bool WindowExist(ProgramWindowDetail windowDetail)
        => this.windowLRUContainer.ExistValue(windowDetail);

    public async Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        this.logger.LogInformation($"Schedule Window {changeState}, {windowDetail.Caption} ({windowDetail.TierIndex})...");
        return changeState switch
        {
            WindowChangeStates.Launch => this.LaunchWindow(windowDetail),
            WindowChangeStates.BringToHead => this.BringToHeadWindow(windowDetail),
            WindowChangeStates.Active => this.ActiveWindow(windowDetail),
            WindowChangeStates.Inactive => this.InactiveWindow(windowDetail),
            WindowChangeStates.Destroy => this.DestroyWindow(windowDetail),
            _ => false
        };
    }

    private bool LaunchWindow(ProgramWindowDetail windowDetail)
    {
        this.logger.LogInformation($"Launch window {windowDetail.WindowId} ...");
        if (windowDetail.WindowState == ProgramWindowStates.Minimized)
            windowDetail.WindowState = windowDetail.LastWindowState;
        this.ActivatedWindow = windowDetail;
        windowDetail.TierIndex = this.GetNewTierIndex();
        return this.windowLRUContainer.Add(windowDetail);
    }

    private bool BringToHeadWindow(ProgramWindowDetail windowDetail)
    {
        if (windowDetail == this.windowLRUContainer.HeadValue &&
            windowDetail.WindowState != ProgramWindowStates.Minimized)
            return false;

        this.logger.LogInformation($"Bring window {windowDetail.WindowId} to head ...");
        if (windowDetail.WindowState == ProgramWindowStates.Minimized)
            windowDetail.WindowState = windowDetail.LastWindowState;

        if (windowDetail != this.windowLRUContainer.HeadValue)
        {
            windowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.BringToHead(windowDetail);
        }

        return true;
    }

    private bool ActiveWindow(ProgramWindowDetail windowDetail)
    {
        if (windowDetail == this.ActivatedWindow &&
            windowDetail.WindowState != ProgramWindowStates.Minimized)
            return false;

        if (windowDetail.WindowState == ProgramWindowStates.Minimized)
            windowDetail.WindowState = windowDetail.LastWindowState;

        if (windowDetail != this.ActivatedWindow)
        {
            this.ActivatedWindow = windowDetail;
            var windowQueue = new Queue<ProgramWindowDetail>();
            windowQueue.Enqueue(windowDetail);
            while (windowQueue.TryDequeue(out var currentWindow))
            {
                this.logger.LogInformation($"Active window {currentWindow.WindowId} ...");
                if (this.WindowExist(currentWindow))
                {
                    this.BringToHeadWindow(currentWindow);
                }

                foreach (var childWindow in currentWindow.GetChildWindowDetails().OrderBy(x => x.TierIndex))
                    windowQueue.Enqueue(childWindow);
            }
        }

        return true;
    }

    private bool InactiveWindow(ProgramWindowDetail windowDetail)
    {
        var previewWindow = windowDetail;
        do
            previewWindow = this.windowLRUContainer.GetPreviousValue(previewWindow);
        while (previewWindow is not null && previewWindow.WindowState == ProgramWindowStates.Minimized);

        if (previewWindow is not null)
        {
            this.logger.LogInformation($"Found preview visible window {previewWindow.WindowId} ...");
            this.ActiveWindow(previewWindow);
        }
        else
            this.ActivatedWindow = null;

        var windowQueue = new Queue<ProgramWindowDetail>();
        windowQueue.Enqueue(windowDetail);
        while (windowQueue.TryDequeue(out var currentWindow))
        {
            this.logger.LogInformation($"Inactive window {currentWindow.WindowId} ...");
            if (currentWindow.WindowState != ProgramWindowStates.Minimized && currentWindow.AllowMinimized)
                currentWindow.WindowState = ProgramWindowStates.Minimized;

            foreach (var childWindow in currentWindow.GetChildWindowDetails().OrderBy(x => x.TierIndex))
                windowQueue.Enqueue(childWindow);
        }

        return true;
    }

    private bool DestroyWindow(ProgramWindowDetail windowDetail)
    {
        this.logger.LogInformation($"Destroy window {windowDetail.WindowId} ...");
        windowDetail.TierIndex = this.WindowTierIndexLowEdge;
        if (this.ActivatedWindow == windowDetail)
            this.InactiveWindow(windowDetail);
        return this.windowLRUContainer.Remove(windowDetail);
    }

    private int GetNewTierIndex()
    {
        var newTierIndex = this.windowLRUContainer.HeadValue?.TierIndex + 1 ?? this.WindowTierIndexLowEdge;
        if (newTierIndex >= this.WindowTierIndexHighEdge)
        {
            this.logger.LogInformation("Reach program superscript, resort all window tier index...");
            var tierIndex = this.WindowTierIndexLowEdge;
            foreach (var window in this.windowLRUContainer.GetValuesFromTail())
            {
                window.TierIndex = tierIndex++;
            }
            newTierIndex = --tierIndex;
        }

        this.logger.LogInformation($"Get new tier index {newTierIndex}.");
        return newTierIndex;
    }
}
