using System;
using System.Reflection;
using HackSystem.Observer.Publisher;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using HackSystem.Web.ProgramSDK.ProgramComponent.ProgramMessages;
using HackSystem.Web.Scheduler.Program.Container;
using HackSystem.Web.Scheduler.Program.IDGenerator;
using HackSystem.Web.Scheduler.Program.Model;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Launcher
{
    public class ProgramLauncher : IProgramLauncher
    {
        private readonly ILogger<ProgramLauncher> logger;
        private readonly IPublisher<ProgramLaunchMessage> publisher;
        private readonly IPIDGenerator pIDGenerator;
        private readonly IProgramContainer programContainer;

        public ProgramLauncher(
            ILogger<ProgramLauncher> logger,
            IPublisher<ProgramLaunchMessage> publisher,
            IPIDGenerator pIDGenerator,
            IProgramContainer programContainer)
        {
            this.logger = logger;
            this.publisher = publisher;
            this.pIDGenerator = pIDGenerator;
            this.programContainer = programContainer;
        }

        public ProcessEntity LaunchProgram(QueryBasicProgramDTO basicProgram)
        {
            var process = new ProcessEntity()
            {
                PID = this.pIDGenerator.GetAvailablePID(),
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
                builder.AddAttribute(2, nameof(ProgramComponentBase.PID), process.PID);
                builder.AddComponentReferenceCapture(3, reference =>
                {
                    process.ProgramComponent = (ProgramComponentBase)reference;
                });
                // 差分算法性能：Component 的 @key 必须与程序ID对应
                builder.SetKey(process.PID);
                // 差分算法要求：标签开启和关闭必须对齐
                builder.CloseComponent();
                builder.CloseRegion();
            };

            this.logger.LogInformation($"程序启动器：添加进程到容器并广播消息...");
            this.programContainer.AddProcess(process);
            this.publisher.Publish(new ProgramLaunchMessage());
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
