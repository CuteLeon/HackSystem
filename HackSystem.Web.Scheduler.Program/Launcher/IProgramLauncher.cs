using HackSystem.Web.Scheduler.Program.Model;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.Scheduler.Program.Launcher;

public interface IProgramLauncher
{
    Task<ProcessDetail> LaunchProgram(QueryBasicProgramDTO basicProgram);
}
