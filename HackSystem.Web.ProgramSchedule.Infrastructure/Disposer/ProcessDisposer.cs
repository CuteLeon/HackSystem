using HackSystem.Web.ProgramSchedule.Application.Disposer;
using HackSystem.Web.ProgramSchedule.Application.Container;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Disposer;

public class ProcessDisposer : IProcessDisposer
{
    private readonly ILogger<IProcessDisposer> logger;
    private readonly IProcessContainer processContainer;

    public ProcessDisposer(
        ILogger<IProcessDisposer> logger,
        IProcessContainer processContainer,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.processContainer = processContainer;
    }

    public async Task DisposeProcess(int processID)
    {
        this.logger.LogInformation($"Process closed: {processID} ID");
        _ = this.processContainer.RemoveProcess(processID);
        GC.Collect();
        await Task.CompletedTask;
    }
}
