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
        var task = await this.taskRepository.FindAsync(taskDetail.TaskID);
        this.hackSystemTaskServer.ExecuteTask(task, this.User?.Identity?.Name);
    }
}
