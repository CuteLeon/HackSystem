using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Application.ScheduleWrappers;

public interface ITaskScheduleWrapper
{
    IEnumerable<(TaskDetail TaskDetail, Action<Schedule> ScheduleAction)> WrapTaskSchedules(IEnumerable<TaskDetail> taskDetails);

    (TaskDetail TaskDetail, Action<Schedule> ScheduleAction) WrapTaskSchedule(TaskDetail taskDetail);
}
