using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.Repository;

public interface ITaskLogRepository : IRepositoryBase<TaskLogDetail>
{
}
