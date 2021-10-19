using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Repository;

public class TaskLogRepository : RepositoryBase<TaskLogDetail>, ITaskLogRepository
{
    public TaskLogRepository(
        ILogger<TaskLogRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }
}
