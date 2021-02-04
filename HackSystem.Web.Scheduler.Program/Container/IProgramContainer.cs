using System.Collections.Generic;
using HackSystem.Web.Scheduler.Program.Model;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public interface IProgramContainer
    {
        IEnumerable<ProcessDetail> GetProcesses();

        void AddProcess(ProcessDetail process);
        
        ProcessDetail RemoveProcess(int pID);

        ProcessDetail GetProcess(int pID);
    }
}
