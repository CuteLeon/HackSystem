using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Container;

public interface IProcessContainer
{
    IEnumerable<ProcessDetail> GetProcesses();

    bool LaunchProcess(ProcessDetail processDetail);

    ProcessDetail? GetProcess(int processID);

    bool DestroyProcess(int processID, out ProcessDetail? processDetail);
}
