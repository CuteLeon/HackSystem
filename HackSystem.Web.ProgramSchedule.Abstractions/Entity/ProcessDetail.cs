namespace HackSystem.Web.ProgramSchedule.Entity;

public class ProcessDetail
{
    public int PID { get; set; }

    public DynamicComponent DynamicProgramComponent { get; set; }

    public ProgramDetail ProgramDetail { get; set; }
}
