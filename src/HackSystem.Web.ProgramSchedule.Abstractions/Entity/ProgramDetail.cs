using System.Reflection;

namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramDetail
{
    public ProgramDetail(
        string id,
        string name,
        string entryAssemblyName,
        string entryTypeName,
        string entryParameter,
        string iconUri,
        bool enabled,
        bool singleInstance,
        bool mandatory)
    {
        this.Id = id;
        this.Name = name;
        this.EntryAssemblyName = entryAssemblyName;
        this.EntryTypeName = entryTypeName;
        this.EntryParameter = entryParameter;
        this.IconUri = iconUri;
        this.Enabled = enabled;
        this.SingleInstance = singleInstance;
        this.Mandatory = mandatory;
    }

    public string Id { get; init; }

    public string Name { get; init; }

    public string EntryAssemblyName { get; init; }

    public string EntryTypeName { get; init; }

    public string EntryParameter { get; init; }

    public Type ProgramEntryType { get; set; }

    public MethodInfo ProgramEntryMethod { get; set; }

    public Type? ProgramEntryComponentType { get; set; }

    public string IconUri { get; init; }

    public bool Enabled { get; init; }

    public bool SingleInstance { get; init; }

    public bool Mandatory { get; init; }

    protected Dictionary<int, ProcessDetail> ProcessDetails { get; init; } = new();

    public bool TryGetProcessDetail(int processId, out ProcessDetail? processDetail)
        => this.ProcessDetails.TryGetValue(processId, out processDetail);

    public IEnumerable<ProcessDetail> GetProcessDetails()
        => this.ProcessDetails.Values.AsEnumerable();

    public bool AddProcessDetail(ProcessDetail processDetail)
        => processDetail.ProgramDetail == this && this.ProcessDetails.TryAdd(processDetail.ProcessId, processDetail);

    public bool RemoveProcessDetail(ProcessDetail processDetail)
        => processDetail.ProgramDetail == this && this.ProcessDetails.Remove(processDetail.ProcessId, out _);
}
