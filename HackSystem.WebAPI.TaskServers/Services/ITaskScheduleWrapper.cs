using FluentScheduler;
using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.Services;

public interface ITaskScheduleWrapper
{
    IEnumerable<(TaskDetail TaskDetail, Action<Schedule> ScheduleAction)> WrapTaskSchedules(IEnumerable<TaskDetail> taskDetails);

    (TaskDetail TaskDetail, Action<Schedule> ScheduleAction) WrapTaskSchedule(TaskDetail taskDetail);
}
