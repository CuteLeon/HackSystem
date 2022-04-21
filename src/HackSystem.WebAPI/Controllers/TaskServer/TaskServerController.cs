using HackSystem.Common;
using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.TaskServer;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = CommonSense.Roles.CommanderRole)]
public class TaskServerController : Controller
{
    private readonly ILogger<TaskServerController> logger;
    private readonly IMapper mapper;
    private readonly IHackSystemTaskServer hackSystemTaskServer;
    private readonly ITaskRepository taskRepository;

    public TaskServerController(
        ILogger<TaskServerController> logger,
        IMapper mapper,
        IHackSystemTaskServer hackSystemTaskServer,
        ITaskRepository taskRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.hackSystemTaskServer = hackSystemTaskServer;
        this.taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        this.logger.LogInformation($"Query tasks...");
        var tasks = await this.taskRepository.QueryTasks();
        this.logger.LogInformation($"Found {tasks.Count()} tasks.");
        var dtos = this.mapper.Map<IEnumerable<TaskDetailResponse>>(tasks);
        return dtos;
    }

    [HttpPost]
    public async Task ExecuteTask(TaskDetailRequest taskDetail)
    {
        var userName = this.User?.Identity?.Name ?? "[Unknown]";
        this.logger.LogInformation($"{userName} executed task {taskDetail.TaskName} [{taskDetail.TaskID}]...");
        var task = await this.taskRepository.FindAsync(taskDetail.TaskID);
        this.hackSystemTaskServer.ExecuteTask(task, userName);
    }

    [HttpPut]
    public async Task UpdateTask(TaskDetailRequest taskDetail)
    {
        var userName = this.User?.Identity?.Name ?? "[Unknown]";
        this.logger.LogInformation($"{userName} updated task {taskDetail.TaskName} [{taskDetail.TaskID}]...");
        var task = await this.taskRepository.FindAsync(taskDetail.TaskID);
        if (taskDetail.Enabled.HasValue)
        {
            task.Enabled = taskDetail.Enabled.Value;
        }

        if (task.Enabled && task.TaskFrequency != (int)TaskFrequency.Manually)
            this.hackSystemTaskServer.LoadTask(task);
        else
            this.hackSystemTaskServer.UnloadTask(task);
        await this.taskRepository.SaveChangesAsync();
    }
}
