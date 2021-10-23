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
        if (processDetail.RemoveWindowDetail(windowDetail))
        {
            _ = await this.publisher.SendRequest(new WindowScheduleRequest(windowDetail, WindowChangeStates.Destory));
        }
        else
        {
            this.logger.LogWarning($"Failed to close window {windowDetail.WindowId} from process {processDetail.ProcessId}...");
        }

        if (processDetail.ProgramDetail.ProgramEntryComponentType is not null &&
            !processDetail.GetWindowDetails().Any())
        {
            this.logger.LogInformation($"Send Destory process command of {processDetail.ProcessId} as all windows destoryed...");
            _ = await this.processDestroyer.DestroyProcess(processDetail.ProcessId);
        }
        GC.Collect();
        this.logger.LogInformation($"Window {windowDetail.WindowId} destroy command handled.");
    }
}
