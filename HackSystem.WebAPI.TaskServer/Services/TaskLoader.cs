using HackSystem.WebAPI.TaskServer.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Services;

public class TaskLoader : ITaskLoader
{
    private readonly ILogger<TaskLoader> logger;
    private readonly ITaskRepository taskDataService;

    public TaskLoader(
        ILogger<TaskLoader> logger,
        ITaskRepository taskDataService)
    {
        this.logger = logger;
        this.taskDataService = taskDataService;
    }

    public IEnumerable<TaskDetail> GetTaskDetails()
    {
        this.logger.LogInformation($"Get task details...");
        var taskDetails = this.taskDataService.QueryEnabledTasks().Result;
        this.logger.LogInformation($"Get {taskDetails.Count()} Task details.");
        return taskDetails;
    }
}
