using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProcessChangeEvent : IIntermediaryEvent
{
    public ProcessChangeEvent(
        ProcessChangeStates changeStates,
        ProcessDetail processDetail)
    {
        this.ChangeStates = changeStates;
        this.ProcessDetail = processDetail;
    }

    public ProcessChangeStates ChangeStates { get; init; }

    public ProcessDetail ProcessDetail { get; init; }
}
