namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProcessDetail
{
    public ProcessDetail(int processId, ProgramDetail programDetail)
        : this(processId, programDetail, DateTime.Now)
    {
    }

    public ProcessDetail(
        int processId,
        ProgramDetail programDetail,
        DateTime launchTime)
    {
        this.ProcessId = processId;
        this.ProgramDetail = programDetail;
        this.LaunchTime = launchTime;
    }

    public int ProcessId { get; init; }

    public DateTime LaunchTime { get; init; }

    public ProgramDetail ProgramDetail { get; init; }

    protected Dictionary<string, ProgramWindowDetail> ProgramWindowDetails { get; init; } = new();

    public bool TryGetWindowDetail(string windowId, out ProgramWindowDetail? windowDetail)
        => this.ProgramWindowDetails.TryGetValue(windowId, out windowDetail);

    public IEnumerable<ProgramWindowDetail> GetWindowDetails()
        => this.ProgramWindowDetails.Values.AsEnumerable();

    public bool AddWindowDetail(ProgramWindowDetail windowDetail)
        => windowDetail.ProcessDetail.Equals(this) && this.ProgramWindowDetails.TryAdd(windowDetail.WindowId, windowDetail);

    public bool RemoveWindowDetail(ProgramWindowDetail windowDetail)
        => windowDetail.ProcessDetail.Equals(this) && this.ProgramWindowDetails.Remove(windowDetail.WindowId, out _);
}
