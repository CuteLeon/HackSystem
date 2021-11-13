using HackSystem.Web.Component.Configurations;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Scheduler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;

public class WindowScheduler : IWindowScheduler
{
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
            WindowChangeStates.TopTier => await this.TopTierWindow(windowDetail),
            WindowChangeStates.NonTopTier => await this.NonTopTierWindow(windowDetail),
            WindowChangeStates.ToggleTopTier => await this.ToggleTopTierWindow(windowDetail),
            _ => await this.CommonScheduleWindow(windowDetail, changeState)
        };

        if (scheduled)
        {
            this.logger.LogInformation($"Window {changeState} scheduled, {windowDetail.Caption} ({windowDetail.TierIndex}).");
            await this.publisher.PublishEvent(new WindowChangeEvent(changeState, windowDetail));
        }
        return scheduled;
    }

    private async Task<bool> TopTierWindow(ProgramWindowDetail windowDetail)
    {
        var windowQueue = new Queue<ProgramWindowDetail>();
        windowQueue.Enqueue(windowDetail);
        while (windowQueue.TryDequeue(out var currentWindow))
        {
            if (currentWindow.StickyTopTier) continue;

            currentWindow.StickyTopTier = true;
            if (await this.windowScheduleContainer.WindowExist(currentWindow))
                await this.windowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Destroy);

            if (!await this.topWindowScheduleContainer.WindowExist(currentWindow))
                await this.topWindowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Launch);

            foreach (var childWindow in currentWindow.GetChildWindowDetails())
                windowQueue.Enqueue(childWindow);
        }

        return await this.topWindowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Active);
    }

    private async Task<bool> NonTopTierWindow(ProgramWindowDetail windowDetail)
    {
        var rootTopWindow = windowDetail;
        while (rootTopWindow.ParentWindow is not null && rootTopWindow.ParentWindow.StickyTopTier)
            rootTopWindow = rootTopWindow.ParentWindow;

        var windowQueue = new Queue<ProgramWindowDetail>();
        windowQueue.Enqueue(rootTopWindow);
        while (windowQueue.TryDequeue(out var currentWindow))
        {
            if (!currentWindow.StickyTopTier) continue;

            currentWindow.StickyTopTier = false;
            if (await this.topWindowScheduleContainer.WindowExist(currentWindow))
                await this.topWindowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Destroy);

            if (!await this.windowScheduleContainer.WindowExist(currentWindow))
                await this.windowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Launch);

            foreach (var childWindow in currentWindow.GetChildWindowDetails())
                windowQueue.Enqueue(childWindow);
        }

        return await this.windowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Active);
    }

    private async Task<bool> ToggleTopTierWindow(ProgramWindowDetail windowDetail)
    {
        return windowDetail.StickyTopTier ?
            await this.NonTopTierWindow(windowDetail) :
            await this.TopTierWindow(windowDetail);
    }

    private async Task<bool> CommonScheduleWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        return await (windowDetail.StickyTopTier ?
            this.topWindowScheduleContainer :
            this.windowScheduleContainer)
                .Schedule(windowDetail, changeState);
    }
}
