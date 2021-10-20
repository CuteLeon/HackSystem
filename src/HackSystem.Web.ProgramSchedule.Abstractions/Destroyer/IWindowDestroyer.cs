using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Destroyer;

public interface IWindowDestroyer
{
    Task DestroyWindow(ProgramWindowDetail windowDetail);
}
