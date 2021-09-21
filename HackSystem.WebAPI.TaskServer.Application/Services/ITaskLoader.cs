using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.Services;

public interface ITaskLoader
{
    IEnumerable<TaskDetail> GetTaskDetails();
}
