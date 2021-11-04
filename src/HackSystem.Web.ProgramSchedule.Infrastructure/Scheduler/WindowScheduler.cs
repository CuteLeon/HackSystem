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
    private readonly IWindowScheduleContainer windowScheduleContainer;
    private readonly IWindowScheduleContainer topWindowScheduleContainer;

    public WindowScheduler(
        ILogger<WindowScheduler> logger,
        IIntermediaryPublisher publisher,
        IWindowScheduleContainer windowScheduleContainer,
        IWindowScheduleContainer topWindowScheduleContainer,
        IOptionsMonitor<WebComponentTierConfiguration> tierConfiguration)
    {
        this.logger = logger;
        this.publisher = publisher;
        this.windowScheduleContainer = windowScheduleContainer;
        this.topWindowScheduleContainer = topWindowScheduleContainer;
        var currentConfig = tierConfiguration.CurrentValue;
        this.windowScheduleContainer.WindowTierIndexLowEdge = currentConfig.BasicProgramEdge;
        this.windowScheduleContainer.WindowTierIndexHighEdge = currentConfig.ProgramDivider;
        this.topWindowScheduleContainer.WindowTierIndexLowEdge = currentConfig.ProgramDivider + 1;
        this.topWindowScheduleContainer.WindowTierIndexHighEdge = currentConfig.TopProgramEdge;
    }

    public async Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        this.logger.LogInformation($"Schedule Window {changeState}, {windowDetail.Caption} ({windowDetail.TierIndex})...");
        var scheduled = changeState switch
        {
            WindowChangeStates.TopTier => await this.TopTierWindow(windowDetail, changeState),
            WindowChangeStates.NonTopTier => await this.NonTopTierWindow(windowDetail, changeState),
            WindowChangeStates.ToggleTopTier => await this.ToggleTopTierWindow(windowDetail, changeState),
            _ => await this.CommonScheduleWindow(windowDetail, changeState)
        };

        if (scheduled)
        {
            this.logger.LogInformation($"Window {changeState} scheduled, {windowDetail.Caption} ({windowDetail.TierIndex}).");
            await this.publisher.PublishEvent(new WindowChangeEvent(changeState, windowDetail));
            this.OnWindowSchedule?.Invoke(windowDetail);
        }
        return scheduled;
    }

    private async Task<bool> TopTierWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        windowDetail.StickyTopTier = true;
        if (await this.windowScheduleContainer.WindowExist(windowDetail))
            await this.windowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Destroy);

        if (await this.topWindowScheduleContainer.WindowExist(windowDetail)) return false;
        return await this.topWindowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Launch);
    }

    private async Task<bool> NonTopTierWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        windowDetail.StickyTopTier = false;
        if (await this.topWindowScheduleContainer.WindowExist(windowDetail))
            await this.topWindowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Destroy);

        if (await this.windowScheduleContainer.WindowExist(windowDetail)) return false;
        return await this.windowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Launch);
    }

    private async Task<bool> ToggleTopTierWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        return windowDetail.StickyTopTier ?
            await this.NonTopTierWindow(windowDetail, changeState) :
            await this.TopTierWindow(windowDetail, changeState);
    }

    private async Task<bool> CommonScheduleWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        return await (windowDetail.StickyTopTier ?
            this.topWindowScheduleContainer :
            this.windowScheduleContainer)
                .Schedule(windowDetail, changeState);
    }
}
