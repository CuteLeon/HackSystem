using System;
using HackSystem.Web.Scheduler.Program.Container;
using HackSystem.Web.Scheduler.Program.Disposer;
using HackSystem.Web.Scheduler.Program.Launcher;
using HackSystem.Web.Scheduler.Program.Options;
using HackSystem.Web.Scheduler.Program.Scheduler;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Web.Scheduler.Program
{
    public static class ProgramSchedulerExtension
    {
        public static IServiceCollection AddHackSystemProgramScheduler(this IServiceCollection services, Action<ProgramSchedulerOptions> configure)
        {
            services
                .Configure(configure)
                .AddSingleton<IProgramContainer, ProgramContainer>()
                .AddSingleton<IProgramLauncher, ProgramLauncher>()
                .AddSingleton<IProgramDisposer, ProgramDisposer>()
                .AddSingleton<IProgramScheduler, ProgramScheduler>();

            return services;
        }
    }
}
