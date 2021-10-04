using System.Reflection;
using HackSystem.Web.ProgramPlatform.Components.ProgramComponent;
using HackSystem.Web.ProgramSchedule.Application.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Web.ProgramSchedule.Application.IDGenerator;
using HackSystem.Web.ProgramSchedule.Application.Launcher;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;

public class ProgramLauncher : IProgramLauncher
{
    private readonly ILogger<ProgramLauncher> logger;
    private readonly IProgramAssemblyLoader programAssemblyLoader;
    private readonly IPIDGenerator pIDGenerator;
    private readonly IProcessContainer processContainer;

    public ProgramLauncher(
        ILogger<ProgramLauncher> logger,
        IProgramAssemblyLoader programAssemblyLoader,
        IPIDGenerator pIDGenerator,
        IProcessContainer processContainer)
    {
        this.logger = logger;
        this.programAssemblyLoader = programAssemblyLoader;
        this.pIDGenerator = pIDGenerator;
        this.processContainer = processContainer;
    }

    public async Task<ProcessDetail?> LaunchProgram(ProgramDetail programDetail)
    {
        if (!this.programAssemblyLoader.CheckAssemblyLoaded(programDetail.AssemblyName))
        {
            this.logger.LogInformation($"Lazy loading assembly {programDetail.AssemblyName} for program {programDetail.Id} as not loeaded...");
            await this.programAssemblyLoader.LoadProgramAssembly(programDetail.Id);
            this.logger.LogInformation($"Lazy loaded assembly {programDetail.AssemblyName} successfully.");
        }

        programDetail.ProgramComponentType = GetProgramComponentType(programDetail.AssemblyName, programDetail.TypeName);
        this.logger.LogInformation($"Launching program of Type={programDetail.ProgramComponentType.FullName}");

        var process = new ProcessDetail()
        {
            PID = this.pIDGenerator.GetAvailablePID(),
            ProgramDetail = programDetail,
        };
        this.logger.LogInformation($"Creating process of program with Name={programDetail.Name} ({process.PID})");

        var result = this.processContainer.LaunchProcess(process);
        if (!result)
        {
            this.logger.LogWarning($"Didn't launch program.");
            return default;
        }

        this.logger.LogInformation($"Launched process and broadcast notification.");
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
