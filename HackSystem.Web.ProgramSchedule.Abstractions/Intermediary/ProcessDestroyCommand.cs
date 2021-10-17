using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProcessDestroyCommand : IIntermediaryCommand
{
    public ProcessDestroyCommand(ProcessDetail processDetail)
    {
        this.ProcessDetail = processDetail;
    }

    public ProcessDetail ProcessDetail { get; init; }
}
