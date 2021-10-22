using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleRequest : IIntermediaryRequest<WindowScheduleResponse>
{
    public WindowScheduleRequest(
        ProgramWindowDetail programWindowDetail,
        WindowChangeStates changeStates)
    {
        this.ProgramWindowDetail = programWindowDetail;
        this.ChangeStates = changeStates;
    }

    public ProgramWindowDetail ProgramWindowDetail { get; init; }

    public WindowChangeStates ChangeStates { get; init; }
}
