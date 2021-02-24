using System.Collections.Generic;
using System.Linq;
using HackSystem.Web.Scheduler.Program.Model;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public class ProcessContainer : IProcessContainer
    {
        private readonly ILogger<ProcessContainer> logger;

        protected Dictionary<int, ProcessDetail> Processes { get; init; } = new Dictionary<int, ProcessDetail>();

        public ProcessContainer(ILogger<ProcessContainer> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<ProcessDetail> GetProcesses()
        {
            this.logger.LogInformation($"Process container, get total of processes => {this.Processes.Count}");
            return this.Processes.Values.AsEnumerable();
        }

        public ProcessDetail GetProcess(int pID)
        {
            this.logger.LogInformation($"Process container, get process => {pID}");
            return this.Processes.TryGetValue(pID, out ProcessDetail process) ? process : default;
        }

        public void AddProcess(ProcessDetail process)
        {
            this.logger.LogInformation($"Process container, add process => {process.PID} ({process.DynamicProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Add(process.PID, process);
            this.logger.LogInformation($"Process container, current total of process => {this.Processes.Count}");
        }

        public ProcessDetail RemoveProcess(int pID)
        {
            var process = this.GetProcess(pID);
            this.logger.LogInformation($"Process container, remove process => {pID}  ({process.DynamicProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Remove(pID);
            this.logger.LogInformation($"Process container, current total of process => {this.Processes.Count}");
            return process;
        }
    }
}
