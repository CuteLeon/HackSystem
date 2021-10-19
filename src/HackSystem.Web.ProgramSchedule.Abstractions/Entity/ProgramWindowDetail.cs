namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProgramWindowDetail
{
    public ProgramWindowDetail(
        string windowId,
        Type programWindowType,
        ProcessDetail processDetail)
    {
        this.WindowId = windowId;
        this.ProgramWindowType = programWindowType;
        this.ProcessDetail = processDetail;
    }

    public string WindowId { get; init; }

    public string Caption { get; set; }

    public Type ProgramWindowType { get; init; }

    public int TierIndex { get; set; }

    public ProcessDetail ProcessDetail { get; init; }
}
