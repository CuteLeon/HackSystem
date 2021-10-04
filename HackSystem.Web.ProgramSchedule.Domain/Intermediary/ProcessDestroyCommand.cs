using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Domain.Intermediary;

public class ProcessDestroyCommand : IIntermediaryCommand
{
    public ProcessDetail ProcessDetail { get; set; }
}
