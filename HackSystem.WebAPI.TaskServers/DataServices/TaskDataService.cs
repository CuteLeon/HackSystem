using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.DataServices;

public class TaskDataService : DataServiceBase<TaskDetail>, ITaskDataService
{
    public TaskDataService(
        ILogger<TaskDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }

    public async Task<IEnumerable<TaskDetail>> QueryTasks()
    {
        return this.AsEnumerable();
    }

    public async Task<IEnumerable<TaskDetail>> QueryEnabledTasks()
    {
        return this.AsQueryable().Where(task => task.Enabled);
    }
}
