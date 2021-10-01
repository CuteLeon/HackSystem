using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Application.Container;

public interface IProcessContainer
{
    IEnumerable<ProcessDetail> GetProcesses();

    void AddProcess(ProcessDetail process);
    
    ProcessDetail RemoveProcess(int pID);

    ProcessDetail GetProcess(int pID);
}
