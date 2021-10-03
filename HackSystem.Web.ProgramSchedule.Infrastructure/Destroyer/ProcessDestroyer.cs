using HackSystem.Web.ProgramSchedule.Application.Destroyer;
using HackSystem.Web.ProgramSchedule.Application.Container;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;

public class ProcessDestroyer : IProcessDestroyer
{
    private readonly ILogger<IProcessDestroyer> logger;
    private readonly IProcessContainer processContainer;

    public ProcessDestroyer(
        ILogger<IProcessDestroyer> logger,
        IProcessContainer processContainer)
    {
        this.logger = logger;
        this.processContainer = processContainer;
    }

    public async Task DisposeProcess(int processID)
    {
        this.logger.LogInformation($"Process closed: {processID} ID");
        _ = this.processContainer.DestroyProcess(processID, out _);
        GC.Collect();
    }
}
