using System;
using System.Diagnostics;
using System.Reflection;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using HackSystem.Web.Scheduler.Program.Container;
using HackSystem.Web.Scheduler.Program.Model;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Launcher
{
    public class ProgramLauncher : IProgramLauncher
    {
        private int availablePID = 0;
        private readonly ILogger<ProgramLauncher> logger;
        private readonly IProgramContainer programContainer;

        public ProgramLauncher(
            ILogger<ProgramLauncher> logger,
            IProgramContainer programContainer)
        {
            this.logger = logger;
            this.programContainer = programContainer;
        }

        private int GetAvailablePID()
            => this.availablePID++;

        public ProcessEntity LaunchProgram(QueryBasicProgramDTO basicProgram)
        {
            var process = new ProcessEntity()
            {
                PID = this.GetAvailablePID(),
            };
            this.logger.LogInformation($"程序启动器：Name={basicProgram.Name} ({process.PID})");

            var programComponentType = this.GetProgramComponentType(basicProgram.AssemblyName, basicProgram.TypeName);
            var programEntity = new ProgramEntity() { Name = basicProgram.Name };

            this.logger.LogInformation($"程序启动器：Type={programComponentType.FullName}");
            process.ProgramRenderFramgment = builder =>
            {
                // 差分算法性能：Region 的序列号必须与程序ID对应
                builder.OpenRegion(process.PID);
                // 差分算法稳定性：Region 内的序列号可以重新开始递增
                builder.OpenComponent(0, programComponentType);
                // Attribute 需要先于其他数据被添加
                builder.AddAttribute(1, nameof(ProgramComponentBase.ProgramEntity), programEntity);
                builder.AddComponentReferenceCapture(2, reference =>
                {
                    process.ProgramComponent = (ProgramComponentBase)reference;
                });
                // 差分算法性能：Component 的 @key 必须与程序ID对应
                builder.SetKey(process.PID);
                // 差分算法要求：标签开启和关闭必须对齐
                builder.CloseComponent();
                builder.CloseRegion();
            };

            this.programContainer.AddProcess(process);
            return process;
        }

        private Type GetProgramComponentType(string assemblyName, string typeName)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));
            var type = assembly.GetType(typeName);
            return !typeof(ProgramComponentBase).IsAssignableFrom(type)
                ? throw new TypeLoadException($"The target program type must derive from {typeof(ProgramComponentBase).Name}")
                : type;
        }
    }
}
