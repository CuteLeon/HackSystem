using HackSystem.WebAPI.TaskServer.Application.Jobs;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Application.Services;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Jobs;

public abstract class TaskJobBase : ITaskJobBase
{
    private readonly ILogger<TaskJobBase> logger;
    private readonly ITaskLogRepository taskLogRepository;

    public bool ManuallyTriggered { get; set; } = false;

    public TaskDetail TaskDetail { get; set; }

    public TaskJobBase(
        ILogger<TaskJobBase> logger,
        ITaskLogRepository taskLogRepository)
    {
        this.logger = logger;
        this.taskLogRepository = taskLogRepository;
    }

    public virtual void Execute()
    {
        var taskLog = new TaskLogDetail
        {
            TaskID = this.TaskDetail.TaskID,
            Parameters = this.TaskDetail.Parameters,
            Trigger = this.ManuallyTriggered ? nameof(this.ManuallyTriggered) : nameof(IHackSystemTaskServer),
            TaskLogStatus = TaskLogStatus.Running,
            TriggerDateTime = DateTime.Now,
            StartDateTime = DateTime.Now,
        };
        this.taskLogRepository.AddAsync(taskLog).ConfigureAwait(false);

        this.logger.LogInformation($"Task Log ID: {taskLog.TaskLogID}, Task {this.TaskDetail.TaskName} [TaskID={this.TaskDetail.TaskID}] starts at {this.TaskDetail.ClassName}.{this.TaskDetail.ProcedureName} method...");
        try
        {
            this.ExecuteTask();
        }
        catch (Exception ex)
        {
            taskLog.TaskLogStatus = TaskLogStatus.Failed;
            taskLog.Exception = ex.ToString();
            this.logger.LogError(ex, $"Task Log ID: {taskLog.TaskLogID}, Task {this.TaskDetail.TaskName} [TaskID={this.TaskDetail.TaskID}] failed.");
            throw;
        }
        finally
        {
            if (taskLog.TaskLogStatus != TaskLogStatus.Failed)
                taskLog.TaskLogStatus = TaskLogStatus.Complete;
            taskLog.FinishDateTime = DateTime.Now;
            this.taskLogRepository.UpdateAsync(taskLog).ConfigureAwait(false);
            this.logger.LogInformation($"Task Log ID: {taskLog.TaskLogID}, Task {this.TaskDetail.TaskName} [TaskID={this.TaskDetail.TaskID}] finished, elapsed: {(taskLog.FinishDateTime - taskLog.StartDateTime).TotalMilliseconds} ms.");
            GC.Collect();
        }
    }

    protected abstract void ExecuteTask();
}
