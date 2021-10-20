using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;

public class ProcessDestroyer : IProcessDestroyer
{
    private readonly ILogger<IProcessDestroyer> logger;
    private readonly IProcessContainer processContainer;
    private readonly IIntermediaryEventPublisher eventPublisher;

    public ProcessDestroyer(
        ILogger<ProcessDestroyer> logger,
        IProcessContainer processContainer,
        IIntermediaryEventPublisher eventPublisher)
    {
        this.logger = logger;
        this.processContainer = processContainer;
        this.eventPublisher = eventPublisher;
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
        await this.eventPublisher.Publish(new ProcessChangeEvent(ProcessChangeStates.Launch, process));
        GC.Collect();
        this.logger.LogInformation($"Destroy process {processID}.");
        return process;
    }
}
