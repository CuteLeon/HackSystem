using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Repository;

public class TaskLogRepository : RepositoryBase<TaskLogDetail>, ITaskLogRepository
{
    public TaskLogRepository(
        ILogger<TaskLogRepository> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
