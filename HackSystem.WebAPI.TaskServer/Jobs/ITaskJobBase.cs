using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Jobs;

public interface ITaskJobBase : IJob
{
    public TaskDetail TaskDetail { get; set; }
}
