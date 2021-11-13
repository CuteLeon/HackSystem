using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Scheduler;

public interface IWindowScheduler
{
    Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState);
}
