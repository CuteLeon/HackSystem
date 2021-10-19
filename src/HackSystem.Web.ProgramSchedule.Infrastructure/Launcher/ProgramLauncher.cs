using System.Reflection;
using System.Text.Json;
using HackSystem.Web.ProgramPlatform.Components.ProgramComponent;
using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.IDGenerator;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Launcher;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;

public class ProgramLauncher : IProgramLauncher
{
    private readonly ILogger<ProgramLauncher> logger;
    private readonly IProgramAssemblyLoader programAssemblyLoader;
    private readonly IPIDGenerator pIDGenerator;
    private readonly IProcessContainer processContainer;
    private readonly IIntermediaryRequestSender requestSender;

    public ProgramLauncher(
        ILogger<ProgramLauncher> logger,
        IProgramAssemblyLoader programAssemblyLoader,
        IPIDGenerator pIDGenerator,
        IProcessContainer processContainer,
        IIntermediaryRequestSender requestSender)
    {
        this.logger = logger;
        this.programAssemblyLoader = programAssemblyLoader;
        this.pIDGenerator = pIDGenerator;
        this.processContainer = processContainer;
        this.requestSender = requestSender;
    }

    public async Task<ProcessDetail?> LaunchProgram(ProgramDetail programDetail)
    {
        if (!this.programAssemblyLoader.CheckAssemblyLoaded(programDetail.EntryAssemblyName))
        {
            this.logger.LogInformation($"Lazy loading assembly {programDetail.EntryAssemblyName} for program {programDetail.Id} as not loeaded...");
            await this.programAssemblyLoader.LoadProgramAssembly(programDetail.Id);
            this.logger.LogInformation($"Lazy loaded assembly {programDetail.EntryAssemblyName} successfully.");
        }

        programDetail.ProgramEntryType = GetProgramEntryType(programDetail.EntryAssemblyName, programDetail.EntryTypeName);
        programDetail.ProgramEntryMethod = GetProgramEntryMethod(programDetail.ProgramEntryType);
        programDetail.ProgramEntryComponentType = programDetail.ProgramEntryMethod.Invoke(default,
            TryGetProgramEntryParameter(programDetail.ProgramEntryMethod, programDetail.EntryParameter, out var parameterObject) ?
                new[] { parameterObject } : Array.Empty<object?>()) is Type entryComponentType ? entryComponentType : default;
        this.logger.LogInformation($"Launching program by entry Method [{programDetail.ProgramEntryMethod.Name}] of Type={programDetail.ProgramEntryType.FullName}");

        var processId = this.pIDGenerator.GetAvailablePID();
        var process = new ProcessDetail(processId, programDetail);
        ProgramWindowDetail programWindowDetail = default;
        if (programDetail.ProgramEntryComponentType is not null)
        {
            if (typeof(ProgramComponentBase).IsAssignableFrom(programDetail.ProgramEntryComponentType))
            {
                programWindowDetail = new ProgramWindowDetail(
                    Guid.NewGuid().ToString(),
                    programDetail.ProgramEntryComponentType,
                    process)
                {
                    Caption = programDetail.Name,
                };
                process.AddWindowDetail(programWindowDetail);
            }
            else
            {
                throw new TypeLoadException($"Program entry component type must derive from {typeof(ProgramComponentBase).Name}.");
            }
        }
        this.logger.LogInformation($"Creating process of program with Name={programDetail.Name} ({process.ProcessId})");

        var result = this.processContainer.LaunchProcess(process);
        if (!result)
        {
            this.logger.LogWarning($"Didn't launch program.");
            return default;
        }

        this.logger.LogInformation($"Launched process.");
        if (programWindowDetail is not null)
        {
            _ = await this.requestSender.Send(new WindowScheduleRequest(programWindowDetail, WindowScheduleStates.Launch));
        }
        return process;
    }

    public static Type GetProgramEntryType(string assemblyName, string typeName)
    {
        var assembly = Assembly.Load(new AssemblyName(assemblyName));
        var type = assembly.GetType(typeName);
        return type;
    }

    public static MethodInfo GetProgramEntryMethod(Type entryType)
    {
        var methodFlags = BindingFlags.Public | BindingFlags.Static;
        var methods = entryType.GetMethods(methodFlags)
            .Where(m => m.ReturnType.Equals(typeof(Type)) || m.ReturnType.Equals(typeof(void)))
            .Where(m => m.GetParameters().Count() <= 1)
            .OrderBy(m => m.ReturnType.Equals(typeof(Type)) ? 0 : 1)
            .ToArray();
        return methods.Count() switch
        {
            0 => throw new EntryPointNotFoundException("Can not find program entry method."),
            1 => methods.First(),
            _ => methods.FirstOrDefault(m => m.Name.Equals("Launch", StringComparison.OrdinalIgnoreCase)) ??
                throw new EntryPointNotFoundException("Entry type have multiple feasible entry method but none of them named as \"Launch\"."),
        };
    }

    public static bool TryGetProgramEntryParameter(MethodInfo entryMethod, string parameter, out object? parameterObject)
    {
        if (!entryMethod.GetParameters().Any())
        {
            parameterObject = default;
            return false;
        }

        var parameterInfo = entryMethod.GetParameters().First();
        if (string.IsNullOrWhiteSpace(parameter))
        {
            parameterObject = parameterInfo.DefaultValue;
            return true;
        }

        parameterObject = JsonSerializer.Deserialize(parameter, parameterInfo.ParameterType);
        return true;
    }
}
