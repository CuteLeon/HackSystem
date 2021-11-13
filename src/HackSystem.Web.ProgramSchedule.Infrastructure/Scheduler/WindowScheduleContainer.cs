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

    public async Task<bool> WindowExist(ProgramWindowDetail windowDetail)
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
            WindowChangeStates.ToggleActive => this.ToggleWindowActive(windowDetail),
            WindowChangeStates.Destroy => this.DestroyWindow(windowDetail),
            _ => false
        };
    }

    private bool LaunchWindow(ProgramWindowDetail windowDetail)
    {
        this.ActivatedWindow = windowDetail;
        windowDetail.TierIndex = this.GetNewTierIndex();
        return this.windowLRUContainer.Add(windowDetail);
    }

    private bool BringToHeadWindow(ProgramWindowDetail windowDetail)
    {
        if (windowDetail.WindowState == ProgramWindowStates.Minimized)
            windowDetail.WindowState = windowDetail.LastWindowState;
        if (windowDetail == this.windowLRUContainer.HeadValue) return false;
        windowDetail.TierIndex = this.GetNewTierIndex();
        this.windowLRUContainer.BringToHead(windowDetail);
        return true;
    }

    private bool ActiveWindow(ProgramWindowDetail windowDetail)
    {
        if (windowDetail.WindowState == ProgramWindowStates.Minimized)
            windowDetail.WindowState = windowDetail.LastWindowState;
        if (this.ActivatedWindow == windowDetail) return false;
        this.ActivatedWindow = windowDetail;
        windowDetail.TierIndex = this.GetNewTierIndex();
        this.windowLRUContainer.BringToHead(windowDetail);
        return true;
    }

    private bool InactiveWindow(ProgramWindowDetail windowDetail)
    {
        if (this.ActivatedWindow != windowDetail) return false;
        if (windowDetail.WindowState == ProgramWindowStates.Normal && windowDetail.AllowMinimized)
            windowDetail.WindowState = ProgramWindowStates.Minimized;
        return true;
    }

    private bool ToggleWindowActive(ProgramWindowDetail windowDetail)
    {
        if (this.ActivatedWindow != windowDetail ||
            windowDetail.WindowState == ProgramWindowStates.Minimized)
            return this.ActiveWindow(windowDetail);
        else
            return this.InactiveWindow(windowDetail);
    }

    private bool DestroyWindow(ProgramWindowDetail windowDetail)
    {
        windowDetail.TierIndex = this.WindowTierIndexLowEdge;
        if (this.ActivatedWindow == windowDetail)
            this.ActivatedWindow = this.windowLRUContainer.HeadValue;
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
        return newTierIndex;
    }
}
