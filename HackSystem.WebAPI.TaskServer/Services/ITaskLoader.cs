using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Services;

public interface ITaskLoader
{
    IEnumerable<TaskDetail> GetTaskDetails();
}
