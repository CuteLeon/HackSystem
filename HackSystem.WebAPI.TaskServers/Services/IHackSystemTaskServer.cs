using HackSystem.WebAPI.TaskServers.Domain.Entity;

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
