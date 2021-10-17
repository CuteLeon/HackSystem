using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowDestroyCommand : IIntermediaryCommand
{
    public WindowDestroyCommand(ProgramWindowDetail programWindowDetail)
    {
        this.ProgramWindowDetail = programWindowDetail;
    }

    public ProgramWindowDetail ProgramWindowDetail { get; set; }
}
