using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProcessChangeEvent : IIntermediaryEvent
{
    public ProcessChangeEvent(
        ProcessChangeStates changeState,
        ProcessDetail processDetail)
    {
        this.ChangeState = changeState;
        this.ProcessDetail = processDetail;
    }

    public ProcessChangeStates ChangeState { get; init; }

    public ProcessDetail ProcessDetail { get; init; }
}
