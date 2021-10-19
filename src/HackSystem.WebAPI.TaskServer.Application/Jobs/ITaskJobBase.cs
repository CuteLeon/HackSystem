using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.Jobs;

public interface ITaskJobBase : IJob
{
    TaskDetail TaskDetail { get; set; }

    string Trigger { get; set; }
}
