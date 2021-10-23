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
    private readonly IIntermediaryPublisher publisher;

    public ProcessDestroyer(
        ILogger<ProcessDestroyer> logger,
        IProcessContainer processContainer,
        IIntermediaryPublisher publisher)
    {
        this.logger = logger;
        this.processContainer = processContainer;
        this.publisher = publisher;
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

        process!.ProgramDetail.RemoveProcessDetail(process);
        await this.publisher.PublishEvent(new ProcessChangeEvent(ProcessChangeStates.Destroy, process));
        GC.Collect();
        this.logger.LogInformation($"Destroy process {processID}.");
        return process;
    }
}
