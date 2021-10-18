using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleRequest : IIntermediaryRequest<WindowScheduleResponse>
{
    public WindowScheduleRequest(
        ProgramWindowDetail programWindowDetail,
        WindowScheduleStates scheduleStates)
    {
        this.ProgramWindowDetail = programWindowDetail;
        this.ScheduleStates = scheduleStates;
    }

    public ProgramWindowDetail ProgramWindowDetail { get; init; }

    public WindowScheduleStates ScheduleStates { get; init; }
}
