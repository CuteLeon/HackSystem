using System.Collections.Generic;
using HackSystem.Web.Scheduler.Program.Model;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public interface IProgramContainer
    {
        List<ProcessEntity> GetProcesses();

        void AddProcess(ProcessEntity process);
        
        ProcessEntity RemoveProcess(int pID);

        ProcessEntity GetProcess(int pID);
    }
}
