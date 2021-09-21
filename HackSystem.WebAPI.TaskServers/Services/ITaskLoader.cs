using HackSystem.WebAPI.TaskServers.Domain.Entity;

namespace HackSystem.WebAPI.TaskServers.Services;

public interface ITaskLoader
{
    IEnumerable<TaskDetail> GetTaskDetails();
}
