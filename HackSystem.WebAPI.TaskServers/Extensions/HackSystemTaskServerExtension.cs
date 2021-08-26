using System;
using HackSystem.WebAPI.Tasks;
using HackSystem.WebAPI.TaskServers.Configurations;
using HackSystem.WebAPI.TaskServers.DataServices;
using HackSystem.WebAPI.TaskServers.Jobs;
using HackSystem.WebAPI.TaskServers.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.Extensions;

    public static class HackSystemTaskServerExtension
    {
        public static IServiceCollection AttachTaskServer(
            this IServiceCollection services,
            TaskServerOptions configuration)
        {
            services
                .Configure(new Action<TaskServerOptions>(options =>
                {
                    options.TaskServerHost = configuration.TaskServerHost;
                }))
                .AddSingleton<IHackSystemTaskServer, HackSystemTaskServer>()
                .AddScoped<ITaskDataService, TaskDataService>()
                .AddScoped<ITaskLogDataService, TaskLogDataService>()
                .AddScoped<ITaskLoader, TaskLoader>()
                .AddScoped<ITaskScheduleWrapper, TaskScheduleWrapper>()
                .AddTransient<ITaskPairParameterWrapper, TaskPairParameterWrapper>()
                .AddTransient<ITaskJsonParameterWrapper, TaskJsonParameterWrapper>()
                .AddTransient<ITaskGenericJob, TaskGenericJob>()
                .AddWebAPITasks();

            return services;
        }

        public static IHost LaunchTaskServer(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var taskServerLauncher = scope.ServiceProvider.GetRequiredService<IHackSystemTaskServer>();

            taskServerLauncher.Launch();
            taskServerLauncher.LoadTasks();

            return host;
        }

        public static IHost ShutdownTaskServer(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var taskServerLauncher = scope.ServiceProvider.GetRequiredService<IHackSystemTaskServer>();

            taskServerLauncher.UnloadTasks();
            taskServerLauncher.Shutdown();

            return host;
        }
    }
