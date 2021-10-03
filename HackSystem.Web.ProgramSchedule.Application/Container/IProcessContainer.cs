using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Enums;

namespace HackSystem.Web.ProgramSchedule.Application.Container;

public interface IProcessContainer
{
    delegate void ProcessChangedHandler(ProcessChangeStates changeStates, ProcessDetail processDetail);

    event ProcessChangedHandler? ProcessChanged;

    IEnumerable<ProcessDetail> GetProcesses();

    void LaunchProcess(ProcessDetail processDetail);

    ProcessDetail? GetProcess(int processID);

    bool DestroyProcess(int processID, out ProcessDetail? processDetail);
}
