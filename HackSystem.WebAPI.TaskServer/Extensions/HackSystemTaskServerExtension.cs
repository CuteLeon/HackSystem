using HackSystem.WebAPI.Tasks;
using HackSystem.WebAPI.TaskServer.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Configuration;
using HackSystem.WebAPI.TaskServer.Jobs;
using HackSystem.WebAPI.TaskServer.Services;
using Microsoft.Extensions.Hosting;

namespace HackSystem.WebAPI.TaskServer.Extensions;

public static class HackSystemTaskServerExtension
{
    public static IServiceCollection AttachTaskServer(
        this IServiceCollection services,
        TaskServerOptions configuration)
    {
        services
            .Configure(new Action<TaskServerOptions>(options => options.TaskServerHost = configuration.TaskServerHost))
            .AddSingleton<IHackSystemTaskServer, HackSystemTaskServer>()
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<ITaskLogRepository, TaskLogRepository>()
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
