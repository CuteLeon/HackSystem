﻿using System.Collections.Generic;
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
            this.logger.LogInformation($"Process container, get total of processes => {this.Processes.Count} count");
            return this.Processes.Values.AsEnumerable();
        }

        public ProcessDetail GetProcess(int pID)
        {
            this.logger.LogInformation($"Process container, get process => {pID} ID");
            return this.Processes.TryGetValue(pID, out ProcessDetail process) ? process : default;
        }

        public void AddProcess(ProcessDetail process)
        {
            this.logger.LogInformation($"Process container, add process => {process.PID} ID ({process.DynamicProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Add(process.PID, process);
            this.logger.LogInformation($"Process container, current total of process => {this.Processes.Count} count");
        }

        public ProcessDetail RemoveProcess(int pID)
        {
            var process = this.GetProcess(pID);
            if (process == null)
            {
                this.logger.LogInformation($"Process container, can not find process => {pID} ID");
            }
            else
            {
                this.logger.LogInformation($"Process container, remove process => {pID} ID ({process.DynamicProgramComponent.GetHashCode().ToString("X")})");
                this.Processes.Remove(pID);
                this.logger.LogInformation($"Process container, current total of process => {this.Processes.Count} count");
            }

            return process;
        }
    }
}
