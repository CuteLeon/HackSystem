using FluentScheduler;
using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.Jobs;

public interface ITaskJobBase : IJob
{
    public TaskDetail TaskDetail { get; set; }
}
