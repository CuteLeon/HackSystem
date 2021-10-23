using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Domain.Intermediary;
using HackSystem.Web.ProgramSchedule.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.IDGenerator;
using HackSystem.Web.ProgramSchedule.Infrastructure.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Infrastructure.Container;
using HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;
using HackSystem.Web.ProgramSchedule.Infrastructure.IDGenerator;
using HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;
using HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Launcher;
using HackSystem.Web.ProgramSchedule.Options;
using HackSystem.Web.ProgramSchedule.Scheduler;

namespace HackSystem.Web.ProgramSchedule;

public static class ProgramSchedulerExtension
{
    public static IServiceCollection AddHackSystemProgramScheduler(this IServiceCollection services, Action<ProgramSchedulerOptions> configure)
    {
        services
            .Configure(configure)
            .AddSingleton<IPIDGenerator, PIDGenerator>()
            .AddSingleton<IProgramAssemblyLoader, ProgramAssemblyLoader>()
            .AddSingleton<IProcessContainer, ProcessContainer>()
            .AddSingleton<IWindowLauncher, WindowLauncher>()
            .AddSingleton<IProgramLauncher, ProgramLauncher>()
            .AddSingleton<IProcessDestroyer, ProcessDestroyer>()
            .AddSingleton<IWindowDestroyer, WindowDestroyer>()
            .AddSingleton<IWindowScheduler, WindowScheduler>()
            .AddIntermediaryEvent<WindowChangeEvent>()
            .AddIntermediaryEvent<ProcessChangeEvent>()
            .AddIntermediaryCommandHandler<ProcessDestroyCommandHandler, ProcessDestroyCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryCommandHandler<WindowDestroyCommandHandler, WindowDestroyCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryCommandHandler<LogoutCommandHandler, LogoutCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryRequestHandler<ProgramLaunchRequestHandler, ProgramLaunchRequest, ProgramLaunchResponse>(ServiceLifetime.Singleton)
            .AddIntermediaryRequestHandler<IWindowScheduleRequestHandler, WindowScheduleRequestHandler, WindowScheduleRequest, WindowScheduleResponse>(ServiceLifetime.Singleton);

        return services;
    }
}
