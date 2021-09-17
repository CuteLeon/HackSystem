using HackSystem.Web.ProgramSchedule.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Disposer;
using HackSystem.Web.ProgramSchedule.IDGenerator;
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
            .AddSingleton<IProgramLauncher, ProgramLauncher>()
            .AddSingleton<IProcessDisposer, ProcessDisposer>()
            .AddSingleton<IProgramScheduler, ProgramScheduler>();

        return services;
    }
}
