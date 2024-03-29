﻿using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.Services;

public interface IHackSystemTaskServer
{
    void Launch();

    void LoadTasks();

    void LoadTask(TaskDetail taskDetail);

    void UnloadTask(TaskDetail taskDetail);

    void UnloadTasks();

    void Shutdown();

    void ExecuteTask(TaskDetail taskDetail, string trigger);
}
