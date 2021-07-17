using System.Linq;
using FluentScheduler;
using HackSystem.WebAPI.Model.Task;
using HackSystem.WebAPI.TaskServers.Configurations;
using HackSystem.WebAPI.TaskServers.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.WebAPI.TaskServers.Services
{
    public class HackSystemTaskServer : IHackSystemTaskServer
    {
        private readonly ILogger<HackSystemTaskServer> logger;
        private readonly ITaskLoader taskLoader;
        private readonly ITaskScheduleWrapper taskScheduleWrapper;
        private readonly TaskServerOptions taskServerOptions;

        public HackSystemTaskServer(
            ILogger<HackSystemTaskServer> logger,
            IOptionsMonitor<TaskServerOptions> optionsMonitor,
            IServiceScopeFactory serviceScopeFactory)
        {
            var serviceScope = serviceScopeFactory.CreateScope();

            this.logger = logger;
            this.taskServerOptions = optionsMonitor.CurrentValue;
            this.taskLoader = serviceScope.ServiceProvider.GetRequiredService<ITaskLoader>();
            this.taskScheduleWrapper = serviceScope.ServiceProvider.GetRequiredService<ITaskScheduleWrapper>();

            JobManager.JobStart += (e) => logger.LogInformation($"Job {e.Name} starts at {e.StartTime}...");
            JobManager.JobEnd += (e) => logger.LogInformation($"Job {e.Name} ends, duration is {e.Duration.TotalMilliseconds} ms, next runs at {e.NextRun}.");
            JobManager.JobException += (e) => logger.LogError(e.Exception, $"Job {e.Name} throws exception.");
        }

        public void Launch()
        {
            this.logger.LogInformation($"Launch Task Server on {taskServerOptions.TaskServerHost}...");

            JobManager.Initialize();

            this.logger.LogInformation($"Task Server launched on {taskServerOptions.TaskServerHost}.");
        }

        public void LoadTasks()
        {
            this.logger.LogInformation($"Load Tasks on {taskServerOptions.TaskServerHost}...");

            this.UnloadTasks();
            var taskDetails = this.taskLoader.GetTaskDetails();
            foreach (var taskDetail in taskDetails)
            {
                this.LoadTask(taskDetail);
            }

            this.logger.LogInformation($"Load {taskDetails.Count()} Tasks...");
        }

        public void LoadTask(TaskDetail taskDetail)
        {
            this.logger.LogInformation($"Load Task {taskDetail.TaskName} on {taskServerOptions.TaskServerHost}...");

            this.UnloadTask(taskDetail);
            var taskSchedule = this.taskScheduleWrapper.WrapTaskSchedule(taskDetail);
            JobManager.AddJob(
                new TaskGenericJob(taskSchedule.TaskDetail),
                taskSchedule.ScheduleAction);

            this.logger.LogInformation($"Task {taskDetail.TaskName} loaded.");
        }

        public void UnloadTask(TaskDetail taskDetail)
        {
            this.logger.LogInformation($"Unload Task {taskDetail.TaskName} on {taskServerOptions.TaskServerHost}...");

            JobManager.RemoveJob(taskDetail.TaskName);

            this.logger.LogInformation($"Task {taskDetail.TaskName} unloaded.");
        }

        public void UnloadTasks()
        {
            this.logger.LogInformation($"Unload Tasks on {taskServerOptions.TaskServerHost}...");

            JobManager.RemoveAllJobs();

            this.logger.LogInformation($"Unload all Tasks...");
        }

        public void Shutdown()
        {
            this.logger.LogInformation($"Shutdown Task Server on {taskServerOptions.TaskServerHost}...");

            JobManager.Stop();

            this.logger.LogInformation($"Task Server shutdowned on {taskServerOptions.TaskServerHost}.");
        }
    }
}
