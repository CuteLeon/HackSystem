using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Application.Jobs;
using HackSystem.WebAPI.TaskServer.Application.ScheduleWrappers;
using HackSystem.WebAPI.TaskServer.Application.Services;
using HackSystem.WebAPI.TaskServer.Domain.Configuration;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using Microsoft.Extensions.Options;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Services;

public class HackSystemTaskServer : IHackSystemTaskServer
{
    private readonly ILogger<HackSystemTaskServer> logger;
    private readonly ITaskLoader taskLoader;
    private readonly ITaskScheduleWrapper taskScheduleWrapper;
    private readonly IOptionsMonitor<TaskServerOptions> options;
    private readonly IServiceProvider serviceProvider;

    public HackSystemTaskServer(
        ILogger<HackSystemTaskServer> logger,
        IOptionsMonitor<TaskServerOptions> options,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.options = options;
        this.serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.taskLoader = this.serviceProvider.GetRequiredService<ITaskLoader>();
        this.taskScheduleWrapper = this.serviceProvider.GetRequiredService<ITaskScheduleWrapper>();

        JobManager.JobStart += (e) => logger.LogInformation($"{nameof(JobManager)}.{nameof(JobManager.JobStart)} => Job [{e.Name}] starts at {e.StartTime}...");
        JobManager.JobEnd += (e) => logger.LogInformation($"{nameof(JobManager)}.{nameof(JobManager.JobStart)} => Job [{e.Name}] ends, duration is {e.Duration.TotalMilliseconds} ms, next runs at {e.NextRun}.");
        JobManager.JobException += (e) => logger.LogError(e.Exception, $"{nameof(JobManager)}.{nameof(JobManager.JobStart)} => Job [{e.Name}] throws exception.");
    }

    public void Launch()
    {
        this.logger.LogInformation($"Launch Task Server on {options.CurrentValue.TaskServerHost}...");

        JobManager.Initialize();

        this.logger.LogInformation($"Task Server launched on {options.CurrentValue.TaskServerHost}.");
    }

    public void LoadTasks()
    {
        this.logger.LogInformation($"Load Tasks on {options.CurrentValue.TaskServerHost}...");

        this.UnloadTasks();
        var taskDetails = this.taskLoader.GetTaskDetails();
        foreach (var taskDetail in taskDetails)
        {
            this.LoadTask(taskDetail);
        }

        this.logger.LogInformation($"Load {taskDetails.Count()} Tasks...");
    }

    public void ExecuteTask(TaskDetail taskDetail)
    {
        this.logger.LogInformation($"Execute Task [{taskDetail.TaskName}] on {options.CurrentValue.TaskServerHost}...");
        var taskJob = this.serviceProvider.GetRequiredService<ITaskGenericJob>();
        taskJob.TaskDetail = taskDetail;
        taskJob.ManuallyTriggered = true;
        JobManager.AddJob(taskJob, schedule => schedule.ToRunNow());
        this.logger.LogInformation($"Task [{taskDetail.TaskName}] executed.");
    }

    public void LoadTask(TaskDetail taskDetail)
    {
        this.logger.LogInformation($"Load Task [{taskDetail.TaskName}] on {options.CurrentValue.TaskServerHost}...");

        this.UnloadTask(taskDetail);
        var taskSchedule = this.taskScheduleWrapper.WrapTaskSchedule(taskDetail);
        var taskJob = this.serviceProvider.GetRequiredService<ITaskGenericJob>();
        taskJob.TaskDetail = taskDetail;
        JobManager.AddJob(taskJob, taskSchedule.ScheduleAction);
        this.logger.LogInformation($"Task [{taskDetail.TaskName}] loaded.");
    }

    public void UnloadTask(TaskDetail taskDetail)
    {
        this.logger.LogInformation($"Unload Task [{taskDetail.TaskName}] on {options.CurrentValue.TaskServerHost}...");

        JobManager.RemoveJob(taskDetail.TaskName);

        this.logger.LogInformation($"Task [{taskDetail.TaskName}] unloaded.");
    }

    public void UnloadTasks()
    {
        this.logger.LogInformation($"Unload Tasks on {options.CurrentValue.TaskServerHost}...");

        JobManager.RemoveAllJobs();

        this.logger.LogInformation($"Unload all Tasks...");
    }

    public void Shutdown()
    {
        this.logger.LogInformation($"Shutdown Task Server on {options.CurrentValue.TaskServerHost}...");

        JobManager.Stop();

        this.logger.LogInformation($"Task Server shutdowned on {options.CurrentValue.TaskServerHost}.");
    }
}
