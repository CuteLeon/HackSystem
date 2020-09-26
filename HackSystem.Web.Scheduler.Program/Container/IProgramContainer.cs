using System.Collections.Generic;
using HackSystem.Web.Scheduler.Program.Model;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public interface IProgramContainer
    {
        List<ProcessEntity> GetProcesses();

        void AddProcess(ProcessEntity process);

        EventCallback OnProcessesUpdate { get; set; }
    }
}
