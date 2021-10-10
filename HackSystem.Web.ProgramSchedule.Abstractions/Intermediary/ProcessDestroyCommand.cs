using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProcessDestroyCommand : IIntermediaryCommand
{
    public ProcessDetail ProcessDetail { get; set; }
}
