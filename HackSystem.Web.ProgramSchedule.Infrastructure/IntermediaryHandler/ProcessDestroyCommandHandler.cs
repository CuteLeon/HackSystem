using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Application.Destroyer;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;

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
        this.logger.LogInformation($"Handle Process destroy command {request.ProcessDetail.PID} ...");
        _ = await this.processDestroyer.DestroyProcess(request.ProcessDetail.PID);
        this.logger.LogInformation($"Process destroy command handled, PID={request.ProcessDetail.PID}.");
        return ValueTuple.Create();
    }
}
