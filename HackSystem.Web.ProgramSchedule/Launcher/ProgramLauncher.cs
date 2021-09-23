using System.Reflection;
using HackSystem.Observer.Publisher;
using HackSystem.Web.ProgramSchedule.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.IDGenerator;
using HackSystem.Web.ProgramSchedule.Model;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;
using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.ProgramSchedule.Launcher;

public class ProgramLauncher : IProgramLauncher
{
    private readonly ILogger<ProgramLauncher> logger;
    private readonly IPublisher<ProgramLaunchMessage> publisher;
    private readonly IProgramAssemblyLoader programAssemblyLoader;
    private readonly IPIDGenerator pIDGenerator;
    private readonly IProcessContainer processContainer;

    public ProgramLauncher(
        ILogger<ProgramLauncher> logger,
        IPublisher<ProgramLaunchMessage> publisher,
        IProgramAssemblyLoader programAssemblyLoader,
        IPIDGenerator pIDGenerator,
        IProcessContainer processContainer)
    {
        this.logger = logger;
        this.publisher = publisher;
        this.programAssemblyLoader = programAssemblyLoader;
        this.pIDGenerator = pIDGenerator;
        this.processContainer = processContainer;
    }

    public async Task<ProcessDetail> LaunchProgram(BasicProgramResponse basicProgram)
    {
        if (!this.programAssemblyLoader.CheckAssemblyLoaded(basicProgram.AssemblyName))
        {
            this.logger.LogInformation($"Lazy loading assembly {basicProgram.AssemblyName} for program {basicProgram.Id} as not loeaded...");
            await this.programAssemblyLoader.LoadProgramAssembly(basicProgram.Id);
            this.logger.LogInformation($"Lazy loaded assembly {basicProgram.AssemblyName} successfully.");
        }

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
