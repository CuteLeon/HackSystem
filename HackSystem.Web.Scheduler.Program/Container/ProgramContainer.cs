using System.Collections.Generic;
using System.Linq;
using HackSystem.Web.Scheduler.Program.Model;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Container
{
    public class ProgramContainer : IProgramContainer
    {
        private readonly ILogger<ProgramContainer> logger;

        protected Dictionary<int, ProcessEntity> Processes { get; init; } = new Dictionary<int, ProcessEntity>();

        public ProgramContainer(ILogger<ProgramContainer> logger)
        {
            this.logger = logger;
        }

        public List<ProcessEntity> GetProcesses()
        {
            this.logger.LogInformation($"程序容器：获取进程集合=> {this.Processes.Count} 个");
            return this.Processes.Values.ToList();
        }

        public void AddProcess(ProcessEntity process)
        {
            this.logger.LogInformation($"程序容器：增加进程=> {process.PID} ({process.ProgramComponent?.GetHashCode().ToString("X")})");
            this.Processes.Add(process.PID, process);
            this.logger.LogInformation($"程序容器：当前进程集合=> {this.Processes.Count} 个");
        }
    }
}
