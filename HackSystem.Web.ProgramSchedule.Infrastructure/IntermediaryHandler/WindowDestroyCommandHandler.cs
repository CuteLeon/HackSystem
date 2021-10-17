using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowDestroyCommandHandler : IIntermediaryCommandHandler<WindowDestroyCommand>
{
    private readonly ILogger<ProcessDestroyCommandHandler> logger;
    private readonly IIntermediaryCommandSender commandSender;

    public WindowDestroyCommandHandler(
        ILogger<ProcessDestroyCommandHandler> logger,
        IIntermediaryCommandSender commandSender)
    {
        this.logger = logger;
        this.commandSender = commandSender;
    }

    public async Task<ValueTuple> Handle(WindowDestroyCommand request, CancellationToken cancellationToken)
    {
        var windowDetail = request.ProgramWindowDetail;
        var processDetail = request.ProgramWindowDetail.ProcessDetail;
        this.logger.LogInformation($"Handle Window destroy command {windowDetail.WindowId} ...");
        if (windowDetail.Equals(processDetail.ProgramEntryWindow))
        {
            this.logger.LogInformation($"Close entry window {windowDetail.WindowId} of process {processDetail.ProcessId}...");
            await this.commandSender.Send(new ProcessDestroyCommand() { ProcessDetail = processDetail });
        }
        else
        {
            this.logger.LogInformation($"Close window {windowDetail.WindowId} of process {processDetail.ProcessId}...");
            if (!processDetail.ProgramWindowDetails.Remove(windowDetail.WindowId, out _))
            {
                this.logger.LogWarning($"Failed to close window {windowDetail.WindowId} from process {processDetail.ProcessId}...");
            }
        }
        this.logger.LogInformation($"Window {request.ProgramWindowDetail.WindowId} destroy command handled.");
        return ValueTuple.Create();
    }
}
