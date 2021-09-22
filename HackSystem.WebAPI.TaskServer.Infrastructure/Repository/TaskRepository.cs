using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Repository;

public class TaskRepository : RepositoryBase<TaskDetail>, ITaskRepository
{
    public TaskRepository(
        ILogger<TaskRepository> logger,
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
