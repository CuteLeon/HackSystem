using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Scheduler;

public interface IWindowScheduler
{
    delegate void WindowScheduleHandler(ProgramWindowDetail programWindowDetail);

    event WindowScheduleHandler? OnWindowSchedule;

    Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState);
}
