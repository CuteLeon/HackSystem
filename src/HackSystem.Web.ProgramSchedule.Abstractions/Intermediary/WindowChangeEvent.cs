using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowChangeEvent : IIntermediaryEvent
{
    public WindowChangeEvent(
        WindowChangeStates changeState,
        ProgramWindowDetail windowDetail)
    {
        this.ChangeState = changeState;
        this.WindowDetail = windowDetail;
    }

    public WindowChangeStates ChangeState { get; init; }

    public ProgramWindowDetail WindowDetail { get; init; }
}
