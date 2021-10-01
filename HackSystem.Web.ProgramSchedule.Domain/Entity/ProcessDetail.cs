namespace HackSystem.Web.ProgramSchedule.Domain.Entity;

public class ProcessDetail
{
    public int PID { get; set; }

    public DynamicComponent DynamicProgramComponent { get; set; }

    public ProgramDetail ProgramDetail { get; set; }
}
