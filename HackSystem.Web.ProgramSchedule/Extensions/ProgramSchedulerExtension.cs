using HackSystem.Web.ProgramSchedule.Application.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Web.ProgramSchedule.Application.Disposer;
using HackSystem.Web.ProgramSchedule.Application.IDGenerator;
using HackSystem.Web.ProgramSchedule.Application.Launcher;
using HackSystem.Web.ProgramSchedule.Application.Scheduler;
using HackSystem.Web.ProgramSchedule.Domain.Options;
using HackSystem.Web.ProgramSchedule.Infrastructure.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Infrastructure.Container;
using HackSystem.Web.ProgramSchedule.Infrastructure.Disposer;
using HackSystem.Web.ProgramSchedule.Infrastructure.IDGenerator;
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
            .AddSingleton<IProcessDisposer, ProcessDisposer>()
            .AddSingleton<IProgramScheduler, ProgramScheduler>();

        return services;
    }
}
