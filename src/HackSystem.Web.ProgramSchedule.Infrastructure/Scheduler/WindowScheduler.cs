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
            WindowChangeStates.ToggleActive => await this.ToggleWindowActive(windowDetail),
            WindowChangeStates.Active => await this.CommonScheduleWindow(windowDetail, changeState),
            _ => await this.SingleScheduleWindow(windowDetail, changeState)
        };

        if (scheduled)
        {
            this.logger.LogInformation($"Window {windowDetail.WindowId} request {changeState} scheduled.");
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

            this.logger.LogInformation($"Sticky window {windowDetail.WindowId} to top...");
            currentWindow.StickyTopTier = true;
            await this.windowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Destroy);
            await this.topWindowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Launch);

            foreach (var childWindow in currentWindow.GetChildWindowDetails())
                windowQueue.Enqueue(childWindow);
        }

        this.logger.LogInformation($"Active window {windowDetail.WindowId} after Sticky to top...");
        await this.topWindowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Active);
        this.logger.LogInformation($"Sticky window {windowDetail.WindowId} to top completed.");
        return true;
    }

    private async Task<bool> NonTopTierWindow(ProgramWindowDetail windowDetail)
    {
        var rootTopWindow = windowDetail;
        while (rootTopWindow.ParentWindow is not null && rootTopWindow.ParentWindow.StickyTopTier)
            rootTopWindow = rootTopWindow.ParentWindow;
        this.logger.LogInformation($"Found root top window {rootTopWindow.WindowId} ...");

        var windowQueue = new Queue<ProgramWindowDetail>();
        windowQueue.Enqueue(rootTopWindow);
        while (windowQueue.TryDequeue(out var currentWindow))
        {
            if (!currentWindow.StickyTopTier) continue;

            this.logger.LogInformation($"Unsticky window {windowDetail.WindowId} from top...");
            currentWindow.StickyTopTier = false;
            await this.topWindowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Destroy);
            await this.windowScheduleContainer.Schedule(currentWindow, WindowChangeStates.Launch);

            foreach (var childWindow in currentWindow.GetChildWindowDetails())
                windowQueue.Enqueue(childWindow);
        }

        this.logger.LogInformation($"Active window {windowDetail.WindowId} after Unsticky from top...");
        await this.windowScheduleContainer.Schedule(windowDetail, WindowChangeStates.Active);
        this.logger.LogInformation($"Unsticky window {windowDetail.WindowId} from top completed.");
        return true;
    }

    private async Task<bool> ToggleTopTierWindow(ProgramWindowDetail windowDetail)
    {
        this.logger.LogInformation($"Toggle window {windowDetail.WindowId} sticky to top or not...");
        return windowDetail.StickyTopTier ?
            await this.NonTopTierWindow(windowDetail) :
            await this.TopTierWindow(windowDetail);
    }

    private async Task<bool> ToggleWindowActive(ProgramWindowDetail windowDetail)
    {
        this.logger.LogInformation($"Toggle window {windowDetail.WindowId} active or not...");
        if (windowDetail.WindowState != ProgramWindowStates.Minimized &&
            (this.windowScheduleContainer.ActivatedWindow == windowDetail ||
            this.topWindowScheduleContainer.ActivatedWindow == windowDetail))
            return await this.SingleScheduleWindow(windowDetail, WindowChangeStates.Inactive);
        else
            return await this.CommonScheduleWindow(windowDetail, WindowChangeStates.Active);
    }

    private async Task<bool> SingleScheduleWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        return await (windowDetail.StickyTopTier ?
            this.topWindowScheduleContainer :
            this.windowScheduleContainer)
                .Schedule(windowDetail, changeState);
    }

    private async Task<bool> CommonScheduleWindow(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        var result = await this.windowScheduleContainer.Schedule(windowDetail, changeState);
        var topResult = await this.topWindowScheduleContainer.Schedule(windowDetail, changeState);
        return result || topResult;
    }
}
