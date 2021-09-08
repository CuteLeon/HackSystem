using HackSystem.Web.ProgramSchedule.Model;

namespace HackSystem.Web.ProgramSchedule.Container;

public interface IProcessContainer
{
    IEnumerable<ProcessDetail> GetProcesses();

    void AddProcess(ProcessDetail process);
    
    ProcessDetail RemoveProcess(int pID);

    ProcessDetail GetProcess(int pID);
}
