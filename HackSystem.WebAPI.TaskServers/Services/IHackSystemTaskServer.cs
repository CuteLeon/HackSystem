using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.Services;

public interface IHackSystemTaskServer
{
    void Launch();

    void LoadTasks();

    void LoadTask(TaskDetail taskDetail);

    void UnloadTask(TaskDetail taskDetail);

    void UnloadTasks();

    void Shutdown();
}
