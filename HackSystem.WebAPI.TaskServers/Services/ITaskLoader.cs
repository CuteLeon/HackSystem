using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.Services;

public interface ITaskLoader
{
    IEnumerable<TaskDetail> GetTaskDetails();
}
