using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Application.Services;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Services;

public class TaskLoader : ITaskLoader
{
    private readonly ILogger<TaskLoader> logger;
    private readonly ITaskRepository taskRepository;

    public TaskLoader(
        ILogger<TaskLoader> logger,
        ITaskRepository taskRepository)
    {
        this.logger = logger;
        this.taskRepository = taskRepository;
    }

    public IEnumerable<TaskDetail> GetTaskDetails()
    {
        this.logger.LogInformation($"Get task details...");
        var taskDetails = this.taskRepository.QueryEnabledTasks().Result;
        this.logger.LogInformation($"Get {taskDetails.Count()} Task details.");
        return taskDetails;
    }
}
