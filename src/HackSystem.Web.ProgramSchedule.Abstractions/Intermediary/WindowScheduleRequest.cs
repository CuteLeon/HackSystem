using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleRequest : IIntermediaryRequest<WindowScheduleResponse>
{
    public WindowScheduleRequest(
        ProgramWindowDetail programWindowDetail,
        WindowChangeStates changeState)
    {
        this.ProgramWindowDetail = programWindowDetail;
        this.ChangeState = changeState;
    }

    public ProgramWindowDetail ProgramWindowDetail { get; init; }

    public WindowChangeStates ChangeState { get; init; }
}
