using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Launcher;

public interface IProgramLauncher
{
    Task<ProcessDetail?> LaunchProgram(ProgramDetail programDetail);
}
