using FluentScheduler;
using HackSystem.WebAPI.TaskServers.Domain.Entity;

namespace HackSystem.WebAPI.TaskServers.Jobs;

public interface ITaskJobBase : IJob
{
    public TaskDetail TaskDetail { get; set; }
}
