using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

namespace HackSystem.Web.ProgramSchedule.Container;

public interface IProcessContainer
{
    delegate void ProcessChangedHandler(ProcessChangeStates changeStates, ProcessDetail processDetail);

    event ProcessChangedHandler? ProcessChanged;

    IEnumerable<ProcessDetail> GetProcesses();

    bool LaunchProcess(ProcessDetail processDetail);

    ProcessDetail? GetProcess(int processID);

    bool DestroyProcess(int processID, out ProcessDetail? processDetail);
}
