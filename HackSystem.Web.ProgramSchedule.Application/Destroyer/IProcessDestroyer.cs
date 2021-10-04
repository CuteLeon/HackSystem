using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Application.Destroyer;

public interface IProcessDestroyer
{
    Task<ProcessDetail?> DestroyProcess(int processID);
}
