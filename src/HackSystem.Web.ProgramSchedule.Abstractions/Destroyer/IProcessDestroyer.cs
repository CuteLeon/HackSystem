using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Destroyer;

public interface IProcessDestroyer
{
    Task<ProcessDetail?> DestroyProcess(int processID);
}
