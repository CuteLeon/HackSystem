using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Task;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.DataServices;

public class TaskLogDataService : DataServiceBase<TaskLogDetail>, ITaskLogDataService
{
    public TaskLogDataService(
        ILogger<TaskLogDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
