using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleResponse
{
    public WindowScheduleResponse(
        WindowChangeStates changeStates,
        bool scheduled)
    {
        this.ChangeStates = changeStates;
        this.Scheduled = scheduled;
    }

    public WindowChangeStates ChangeStates { get; init; }

    public bool Scheduled { get; init; }
}
