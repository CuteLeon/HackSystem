using System.Threading.Tasks;
using HackSystem.Web.ProgramSchedule.Model;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.ProgramSchedule.Launcher;

public interface IProgramLauncher
{
    Task<ProcessDetail> LaunchProgram(BasicProgramDTO basicProgram);
}
