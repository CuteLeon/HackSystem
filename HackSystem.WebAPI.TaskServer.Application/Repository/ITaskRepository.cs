using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.Repository;

public interface ITaskRepository : IRepositoryBase<TaskDetail>
{
    Task<IEnumerable<TaskDetail>> QueryTasks();

    Task<IEnumerable<TaskDetail>> QuerySchedulableTasks();
}
