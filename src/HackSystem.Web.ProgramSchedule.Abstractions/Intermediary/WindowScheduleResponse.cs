using HackSystem.Web.ProgramSchedule.Abstractions.Enums;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleResponse
{
    public WindowScheduleResponse(
        WindowScheduleStates scheduleStates,
        bool scheduled)
    {
        this.ScheduleStates = scheduleStates;
        this.Scheduled = scheduled;
    }

    public WindowScheduleStates ScheduleStates { get; init; }

    public bool Scheduled { get; init; }
}
