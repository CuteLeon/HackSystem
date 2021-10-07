using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Domain.Intermediary;
using HackSystem.Web.ProgramSchedule.Application.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Web.ProgramSchedule.Application.Destroyer;
using HackSystem.Web.ProgramSchedule.Application.IDGenerator;
using HackSystem.Web.ProgramSchedule.Application.Launcher;
using HackSystem.Web.ProgramSchedule.Application.Scheduler;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;
using HackSystem.Web.ProgramSchedule.Domain.Options;
using HackSystem.Web.ProgramSchedule.Infrastructure.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Infrastructure.Container;
using HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;
using HackSystem.Web.ProgramSchedule.Infrastructure.IDGenerator;
using HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;
using HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;

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
            .AddSingleton<IProgramLauncher, ProgramLauncher>()
            .AddSingleton<IProcessDestroyer, ProcessDestroyer>()
            .AddSingleton<IProgramScheduler, ProgramScheduler>()
            .AddIntermediaryCommandHandler<ProcessDestroyCommandHandler, ProcessDestroyCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryCommandHandler<LogoutCommandHandler, LogoutCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryRequestHandler<ProgramLaunchRequestHandler, ProgramLaunchRequest, ProgramLaunchResponse>(ServiceLifetime.Singleton);

        return services;
    }
}
