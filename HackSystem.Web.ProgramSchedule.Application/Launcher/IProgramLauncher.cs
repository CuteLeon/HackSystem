using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Application.Launcher;

public interface IProgramLauncher
{
    Task<ProcessDetail> LaunchProgram(ProgramDetail programDetail);
}
