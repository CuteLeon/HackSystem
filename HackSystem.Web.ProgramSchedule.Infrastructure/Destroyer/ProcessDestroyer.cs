using HackSystem.Web.ProgramSchedule.Application.Destroyer;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;

public class ProcessDestroyer : IProcessDestroyer
{
    private readonly ILogger<IProcessDestroyer> logger;
    private readonly IProcessContainer processContainer;

    public ProcessDestroyer(
        ILogger<ProcessDestroyer> logger,
        IProcessContainer processContainer)
    {
        this.logger = logger;
        this.processContainer = processContainer;
    }

    public async Task<ProcessDetail?> DestroyProcess(int processID)
    {
        this.logger.LogInformation($"Destroy Process of PID = {processID}...");
        var result = this.processContainer.DestroyProcess(processID, out var process);
        if (!result)
        {
            this.logger.LogWarning($"Didn't destroy process {processID}.");
            return default;
        }

        GC.Collect();
        this.logger.LogInformation($"Destroy process {processID}.");
        return process;
    }
}
