using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class ProcessDestroyCommandHandler : IIntermediaryCommandHandler<ProcessDestroyCommand>
{
    private readonly ILogger<ProcessDestroyCommandHandler> logger;
    private readonly IProcessDestroyer processDestroyer;

    public ProcessDestroyCommandHandler(
        ILogger<ProcessDestroyCommandHandler> logger,
        IProcessDestroyer processDestroyer)
    {
        this.logger = logger;
        this.processDestroyer = processDestroyer;
    }

    public async Task<ValueTuple> Handle(ProcessDestroyCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Process destroy command {request.ProcessDetail.ProcessId} ...");
        _ = await this.processDestroyer.DestroyProcess(request.ProcessDetail.ProcessId);
        this.logger.LogInformation($"Process destroy command handled, PID={request.ProcessDetail.ProcessId}.");
        return ValueTuple.Create();
    }
}
