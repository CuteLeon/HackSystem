using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProgramLaunchResponse
{
    public ProgramLaunchResponse(ProcessDetail processDetail)
    {
        this.ProcessDetail = processDetail;
    }

    public ProcessDetail ProcessDetail { get; set; }
}
