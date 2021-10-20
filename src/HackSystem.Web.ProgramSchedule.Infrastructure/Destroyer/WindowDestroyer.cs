using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;

public class WindowDestroyer : IWindowDestroyer
{
    private readonly ILogger<WindowDestroyer> logger;
    private readonly IIntermediaryCommandSender commandSender;
    private readonly IIntermediaryRequestSender requestSender;

    public WindowDestroyer(
        ILogger<WindowDestroyer> logger,
        IIntermediaryCommandSender commandSender,
        IIntermediaryRequestSender requestSender)
    {
        this.logger = logger;
        this.commandSender = commandSender;
        this.requestSender = requestSender;
    }

    public async Task DestroyWindow(ProgramWindowDetail windowDetail)
    {
        var processDetail = windowDetail.ProcessDetail;
        this.logger.LogInformation($"Handle Window destroy command: Window {windowDetail.WindowId} of Process {processDetail.ProcessId} ...");
        if (processDetail.RemoveWindowDetail(windowDetail))
        {
            _ = await this.requestSender.Send(new WindowScheduleRequest(windowDetail, WindowScheduleStates.Destory));
        }
        else
        {
            this.logger.LogWarning($"Failed to close window {windowDetail.WindowId} from process {processDetail.ProcessId}...");
        }

        if (processDetail.ProgramDetail.ProgramEntryComponentType is not null &&
            !processDetail.GetWindowDetails().Any())
        {
            this.logger.LogInformation($"Send Destory process command of {processDetail.ProcessId} as all windows destoryed...");
            await this.commandSender.Send(new ProcessDestroyCommand(processDetail));
        }
        GC.Collect();
        this.logger.LogInformation($"Window {windowDetail.WindowId} destroy command handled.");
    }
}
