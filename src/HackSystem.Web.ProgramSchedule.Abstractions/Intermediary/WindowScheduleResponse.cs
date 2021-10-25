using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowScheduleResponse
{
    public WindowScheduleResponse(
        WindowChangeStates changeState,
        bool scheduled)
    {
        this.ChangeState = changeState;
        this.Scheduled = scheduled;
    }

    public WindowChangeStates ChangeState { get; init; }

    public bool Scheduled { get; init; }
}
