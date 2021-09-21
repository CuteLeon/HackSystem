using HackSystem.Common;
using HackSystem.WebAPI.TaskServer.Repository;
using HackSystem.WebDataTransfer.TaskServer;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.TaskServer;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = CommonSense.Roles.CommanderRole)]
public class TaskServerController : Controller
{
    private readonly ILogger<TaskServerController> logger;
    private readonly IMapper mapper;
    private readonly ITaskRepository taskDataService;

    public TaskServerController(
        ILogger<TaskServerController> logger,
        IMapper mapper,
        ITaskRepository taskDataService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.taskDataService = taskDataService;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskDetailDTO>> QueryTasks()
    {
        this.logger.LogInformation($"Query tasks...");
        var tasks = await this.taskDataService.QueryTasks();
        this.logger.LogInformation($"Found {tasks.Count()} tasks.");
        var dtos = this.mapper.Map<IEnumerable<TaskDetailDTO>>(tasks);
        return dtos;
    }
}
