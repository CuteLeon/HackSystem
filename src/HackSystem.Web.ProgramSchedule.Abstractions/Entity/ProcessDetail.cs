namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProcessDetail
{
    public ProcessDetail(
        int processId,
        ProgramDetail programDetail)
    {
        ProcessId = processId;
        ProgramDetail = programDetail;
    }

    public int ProcessId { get; init; }

    public ProgramDetail ProgramDetail { get; init; }

    protected Dictionary<string, ProgramWindowDetail> ProgramWindowDetails { get; init; } = new();

    public IEnumerable<ProgramWindowDetail> GetWindowDetails()
        => this.ProgramWindowDetails.Values.AsEnumerable();

    public bool AddWindowDetail(ProgramWindowDetail windowDetail)
        => windowDetail.ProcessDetail.Equals(this) && this.ProgramWindowDetails.TryAdd(windowDetail.WindowId, windowDetail);

    public bool RemoveWindowDetail(ProgramWindowDetail windowDetail)
        => windowDetail.ProcessDetail.Equals(this) && this.ProgramWindowDetails.Remove(windowDetail.WindowId, out _);
}
