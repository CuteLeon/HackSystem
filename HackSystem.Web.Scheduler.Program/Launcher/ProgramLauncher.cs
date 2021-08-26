using System;
using System.Reflection;
using System.Threading.Tasks;
using HackSystem.Observer.Publisher;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;
using HackSystem.Web.Scheduler.Program.Container;
using HackSystem.Web.Scheduler.Program.IDGenerator;
using HackSystem.Web.Scheduler.Program.Model;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Launcher;

public class ProgramLauncher : IProgramLauncher
{
    private readonly ILogger<ProgramLauncher> logger;
    private readonly IPublisher<ProgramLaunchMessage> publisher;
    private readonly IPIDGenerator pIDGenerator;
    private readonly IProcessContainer processContainer;

    public ProgramLauncher(
        ILogger<ProgramLauncher> logger,
        IPublisher<ProgramLaunchMessage> publisher,
        IPIDGenerator pIDGenerator,
        IProcessContainer processContainer)
    {
        this.logger = logger;
        this.publisher = publisher;
        this.pIDGenerator = pIDGenerator;
        this.processContainer = processContainer;
    }

    public async Task<ProcessDetail> LaunchProgram(QueryBasicProgramDTO basicProgram)
    {
        var programEntity = new ProgramEntity()
        {
            Name = basicProgram.Name,
            ProgramComponentType = GetProgramComponentType(basicProgram.AssemblyName, basicProgram.TypeName),
        };
        this.logger.LogInformation($"Program launcher: Type={programEntity.ProgramComponentType.FullName}");

        var process = new ProcessDetail()
        {
            PID = this.pIDGenerator.GetAvailablePID(),
            ProgramEntity = programEntity,
        };
        this.logger.LogInformation($"Program launcher: Name={basicProgram.Name} ({process.PID})");

        this.processContainer.AddProcess(process);
        await this.publisher.Publish(new ProgramLaunchMessage() { PID = process.PID });
        this.logger.LogInformation($"Program launcher: Add processs and broadcast notification.");

        return process;
    }

    private static Type GetProgramComponentType(string assemblyName, string typeName)
    {
        var assembly = Assembly.Load(new AssemblyName(assemblyName));
        var type = assembly.GetType(typeName);
        return !typeof(ProgramComponentBase).IsAssignableFrom(type)
            ? throw new TypeLoadException($"The target program type must derive from {typeof(ProgramComponentBase).Name}")
            : type;
    }
}
