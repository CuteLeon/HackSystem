using System.Collections.Generic;
using System.Linq;
using HackSystem.Web.Scheduler.Program.Model;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public class ProgramContainer : IProgramContainer
    {
        private readonly ILogger<ProgramContainer> logger;

        protected Dictionary<int, ProcessDetail> Processes { get; init; } = new Dictionary<int, ProcessDetail>();

        public ProgramContainer(ILogger<ProgramContainer> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<ProcessDetail> GetProcesses()
        {
            this.logger.LogInformation($"程序容器：获取进程集合=> {this.Processes.Count} 个");
            return this.Processes.Values.AsEnumerable();
        }

        public ProcessDetail GetProcess(int pID)
        {
            this.logger.LogInformation($"程序容器：获取进程=> {pID}");
            return this.Processes.TryGetValue(pID, out ProcessDetail process) ? process : default;
        }

        public void AddProcess(ProcessDetail process)
        {
            this.logger.LogInformation($"程序容器：增加进程=> {process.PID} ({process.ProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Add(process.PID, process);
            this.logger.LogInformation($"程序容器：当前进程集合=> {this.Processes.Count} 个");
        }

        public ProcessDetail RemoveProcess(int pID)
        {
            var process = this.GetProcess(pID);
            this.logger.LogInformation($"程序容器：移除进程=> {pID}  ({process.ProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Remove(pID);
            this.logger.LogInformation($"程序容器：当前进程集合=> {this.Processes.Count} 个");
            return process;
        }
    }
}
