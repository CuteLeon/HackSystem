using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Launcher;

public interface IWindowLauncher
{
    Task<ProgramWindowDetail> LaunchWindow(ProcessDetail process, Type windowComponentType);
}
