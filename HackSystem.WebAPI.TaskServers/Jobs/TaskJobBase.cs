using System;
using FluentScheduler;
using HackSystem.WebAPI.Model.Task;
using HackSystem.WebAPI.TaskServers.DataServices;
using HackSystem.WebAPI.TaskServers.Services;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.Jobs
{
    public abstract class TaskJobBase : IJob
    {
        private readonly ILogger<TaskJobBase> logger;
        private readonly ITaskLogDataService taskLogDataService;

        public TaskDetail TaskDetail { get; set; }

        public TaskJobBase(
            ILogger<TaskJobBase> logger,
            ITaskLogDataService taskLogDataService)
        {
            this.logger = logger;
            this.taskLogDataService = taskLogDataService;
        }

        public void Execute()
        {
            var taskLog = new TaskLogDetail
            {
                TaskID = this.TaskDetail.TaskID,
                Parameters = this.TaskDetail.Parameters,
                Trigger = nameof(HackSystemTaskServer),
                TaskLogStatus = TaskLogStatus.Running,
                TriggerDateTime = DateTime.Now,
                StartDateTime = DateTime.Now,
            };
            this.taskLogDataService.AddAsync(taskLog).ConfigureAwait(false);

            try
            {
                ExecuteTask();
            }
            catch (Exception ex)
            {
                taskLog.TaskLogStatus = TaskLogStatus.Failed;
                taskLog.Exception = ex.ToString();

                throw;
            }
            finally
            {
                if (taskLog.TaskLogStatus != TaskLogStatus.Failed)
                    taskLog.TaskLogStatus = TaskLogStatus.Complete;
                taskLog.FinishDateTime = DateTime.Now;
                this.taskLogDataService.UpdateAsync(taskLog).ConfigureAwait(false);
            }
        }

        protected abstract void ExecuteTask();
    }
}
