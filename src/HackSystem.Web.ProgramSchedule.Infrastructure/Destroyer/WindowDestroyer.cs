using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;

public class WindowDestroyer : IWindowDestroyer
{
    private readonly ILogger<WindowDestroyer> logger;
    private readonly IProcessDestroyer processDestroyer;
    private readonly IIntermediaryPublisher publisher;

    public WindowDestroyer(
        ILogger<WindowDestroyer> logger,
        IProcessDestroyer processDestroyer,
        IIntermediaryPublisher publisher)
    {
        this.logger = logger;
        this.processDestroyer = processDestroyer;
        this.publisher = publisher;
    }

    public async Task DestroyWindow(ProgramWindowDetail windowDetail)
    {
        var processDetail = windowDetail.ProcessDetail;
        this.logger.LogInformation($"Handle Window destroy command: Window {windowDetail.WindowId} of Process {processDetail.ProcessId} ...");
        await this.DestroyWindowInternal(windowDetail);

        if (processDetail.ProgramDetail.ProgramEntryComponentType is not null &&
            !processDetail.GetWindowDetails().Any())
        {
            this.logger.LogInformation($"Send Destroy process command of {processDetail.ProcessId} as all windows destroyed...");
            _ = await this.processDestroyer.DestroyProcess(processDetail.ProcessId);
        }
        GC.Collect();
        this.logger.LogInformation($"Window {windowDetail.WindowId} destroy command handled.");
    }

    protected async Task DestroyWindowInternal(ProgramWindowDetail windowDetail)
    {
        Stack<ProgramWindowDetail> windowStack = new(new[] { windowDetail });
        while (windowStack.TryPop(out var currentWindow))
        {
            foreach (var childWindow in currentWindow.GetChildWindowDetails())
                windowStack.Push(childWindow);

            if (currentWindow.SetParentWindow(null) &&
                currentWindow.ProcessDetail.RemoveWindowDetail(currentWindow))
            {
                this.logger.LogInformation($"Send Destroy window command of {currentWindow.WindowId}...");
                _ = await this.publisher.SendRequest(new WindowScheduleRequest(currentWindow, WindowChangeStates.Destroy));
            }
        }
    }
}
