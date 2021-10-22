using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class WindowChangeEvent : IIntermediaryEvent
{
    public WindowChangeEvent(
        WindowChangeStates changeStates,
        ProgramWindowDetail windowDetail)
    {
        this.ChangeStates = changeStates;
        this.WindowDetail = windowDetail;
    }

    public WindowChangeStates ChangeStates { get; init; }

    public ProgramWindowDetail WindowDetail { get; init; }
}
