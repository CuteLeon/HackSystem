using HackSystem.Web.ProgramSchedule.Model;
using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.ProgramSchedule.Launcher;

public interface IProgramLauncher
{
    Task<ProcessDetail> LaunchProgram(BasicProgramResponse basicProgram);
}
