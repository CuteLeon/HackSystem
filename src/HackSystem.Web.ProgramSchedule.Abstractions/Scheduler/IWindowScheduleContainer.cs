using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Scheduler;

public interface IWindowScheduleContainer
{
    int WindowTierIndexLowEdge { get; set; }

    int WindowTierIndexHighEdge { get; set; }

    ProgramWindowDetail? ActivatedWindow { get; }

    bool WindowExist(ProgramWindowDetail windowDetail);

    Task<bool> Schedule(ProgramWindowDetail windowDetail, WindowChangeStates changeState);
}
