using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Destroyer;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class LogoutCommandHandler : IIntermediaryCommandHandler<LogoutCommand>
{
    private readonly ILogger<LogoutCommandHandler> logger;
    private readonly IProcessContainer processContainer;
    private readonly IProcessDestroyer processDestroyer;

    public LogoutCommandHandler(
        ILogger<LogoutCommandHandler> logger,
        IProcessContainer processContainer,
        IProcessDestroyer processDestroyer)
    {
        this.logger = logger;
        this.processContainer = processContainer;
        this.processDestroyer = processDestroyer;
    }

    public async Task<ValueTuple> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Logout command ...");
        var processes = this.processContainer.GetProcesses();
        foreach (var process in processes)
        {
            _ = await this.processDestroyer.DestroyProcess(process.PID);
        }
        this.logger.LogInformation($"Logout command handled, total {processes.Count()} processes.");
        return ValueTuple.Create();
    }
}
