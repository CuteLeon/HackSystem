using System.Collections.Concurrent;
using System.Collections.Immutable;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Enums;
using static HackSystem.Web.ProgramSchedule.Application.Container.IProcessContainer;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Container;

public class ProcessContainer : IProcessContainer
{
    private readonly ILogger<ProcessContainer> logger;

    protected ConcurrentDictionary<int, ProcessDetail> Processes { get; init; } = new();

    public event ProcessChangedHandler? ProcessChanged;

    public ProcessContainer(ILogger<ProcessContainer> logger)
    {
        this.logger = logger;
    }

    public IEnumerable<ProcessDetail> GetProcesses()
    {
        this.logger.LogInformation($"Process container, get total of processes => {this.Processes.Count} count");
        return this.Processes.Values.ToImmutableArray();
    }

    public ProcessDetail? GetProcess(int processID)
    {
        this.logger.LogInformation($"Process container, get process => {processID} ID");
        return this.Processes.TryGetValue(processID, out ProcessDetail? process) ? process : default;
    }

    public void LaunchProcess(ProcessDetail processDetail)
    {
        this.logger.LogInformation($"Process container, add process => {processDetail.PID} ID");
        this.Processes.TryAdd(processDetail.PID, processDetail);
        this.ProcessChanged?.Invoke(ProcessChangeStates.Launch, processDetail);
    }

    public bool DestroyProcess(int processID, out ProcessDetail? processDetail)
    {
        this.logger.LogInformation($"Process container, remove process => {processID} ID");
        var result = this.Processes.TryRemove(processID, out processDetail);
        this.ProcessChanged?.Invoke(ProcessChangeStates.Destroy, processDetail);
        return result;
    }
}
