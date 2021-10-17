using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Repository;

public class TaskRepository : RepositoryBase<TaskDetail>, ITaskRepository
{
    public TaskRepository(
        ILogger<TaskRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<IEnumerable<TaskDetail>> QueryTasks()
    {
        return this.AsEnumerable();
    }

    public async Task<IEnumerable<TaskDetail>> QuerySchedulableTasks()
    {
        return this.AsQueryable().Where(task => task.Enabled && task.TaskFrequency != TaskFrequency.Manually);
    }
}
