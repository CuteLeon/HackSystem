using HackSystem.Web.ProgramSchedule.Launcher;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class ProgramLaunchRequestHandler : IIntermediaryRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>
{
    private readonly ILogger<ProgramLaunchRequestHandler> logger;
    private readonly IProgramLauncher programLauncher;

    public ProgramLaunchRequestHandler(
        ILogger<ProgramLaunchRequestHandler> logger,
        IProgramLauncher programLauncher)
    {
        this.logger = logger;
        this.programLauncher = programLauncher;
    }

    public async Task<ProgramLaunchResponse> Handle(ProgramLaunchRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Program launch request {request.ProgramDetail.Name} ...");
        var processDetail = await this.programLauncher.LaunchProgram(request.ProgramDetail);
        this.logger.LogInformation($"Program launch request handled, PID={processDetail.ProcessId}.");
        return new ProgramLaunchResponse() { ProcessDetail = processDetail };
    }
}
