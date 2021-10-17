namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProcessDetail
{
    public int ProcessId { get; set; }

    public ProgramDetail ProgramDetail { get; set; }

    public ProgramWindowDetail ProgramEntryWindow { get; set; }

    public Dictionary<string, ProgramWindowDetail> ProgramWindowDetails { get; set; }
}
