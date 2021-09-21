using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using HackSystem.WebAPI.TaskServer.Interfaces;

namespace HackSystem.WebAPI.TaskServer.Services;

public class TaskScheduleWrapper : ITaskScheduleWrapper
{
    public IEnumerable<(TaskDetail TaskDetail, Action<Schedule> ScheduleAction)> WrapTaskSchedules(IEnumerable<TaskDetail> taskDetails)
    {
        return taskDetails.Select(task => this.WrapTaskSchedule(task));
    }

    public (TaskDetail TaskDetail, Action<Schedule> ScheduleAction) WrapTaskSchedule(TaskDetail taskDetail)
    {
        void scheduleAction(Schedule schedule)
        {
            schedule.WithName(taskDetail.TaskName);

            switch (taskDetail.TaskFrequency)
            {
                case TaskFrequency.Manually:
                    {
                        schedule.ToRunNow();

                        break;
                    }
                case TaskFrequency.Once:
                    {
                        if (taskDetail.ExecuteDateTime != default)
                        {
                            if (taskDetail.ExecuteDateTime < DateTime.Now) return;

                            schedule.ToRunOnceAt(taskDetail.ExecuteDateTime.Hour, taskDetail.ExecuteDateTime.Minute)
                                .DelayFor(Convert.ToInt32((taskDetail.ExecuteDateTime - DateTime.Now).TotalDays)).Days();
                        }
                        else if (taskDetail.FirstInterval != default)
                        {
                            schedule.ToRunOnceIn(Convert.ToInt32(taskDetail.FirstInterval.TotalSeconds)).Seconds();
                        }
                        else
                        {
                            schedule.ToRunNow();
                        }

                        break;
                    }
                case TaskFrequency.Automatically:
                    {
                        schedule
                            .ToRunEvery(Convert.ToInt32(taskDetail.AutomaticInterval.TotalSeconds)).Seconds()
                            .DelayFor(Convert.ToInt32(taskDetail.FirstInterval.TotalSeconds)).Seconds();

                        break;
                    }
                case TaskFrequency.Daily:
                    {
                        schedule
                            .ToRunEvery(Convert.ToInt32(taskDetail.AutomaticInterval.TotalDays)).Days()
                            .At(taskDetail.ExecuteDateTime.Hour, taskDetail.ExecuteDateTime.Minute);

                        break;
                    }
                case TaskFrequency.Weekly:
                    {
                        schedule
                            .ToRunEvery(Convert.ToInt32(taskDetail.AutomaticInterval.TotalDays / 7)).Weeks().On(taskDetail.ExecuteDateTime.DayOfWeek)
                            .At(taskDetail.ExecuteDateTime.Hour, taskDetail.ExecuteDateTime.Minute);

                        break;
                    }
                case TaskFrequency.Monthly:
                    {
                        schedule
                            .ToRunEvery(0).Months().On(taskDetail.ExecuteDateTime.Day)
                            .At(taskDetail.ExecuteDateTime.Hour, taskDetail.ExecuteDateTime.Minute);

                        break;
                    }
                case TaskFrequency.Yearly:
                    {
                        var unit = schedule.ToRunEvery(12).Months();
                        unit.DelayFor(DateTime.Now.Month < taskDetail.ExecuteDateTime.Month ? taskDetail.ExecuteDateTime.Month - DateTime.Now.Month : 12 + taskDetail.ExecuteDateTime.Month - DateTime.Now.Month).Months();
                        unit.On(taskDetail.ExecuteDateTime.Day).At(taskDetail.ExecuteDateTime.Hour, taskDetail.ExecuteDateTime.Minute);

                        break;
                    }
            }

            if (taskDetail.Enabled) schedule.Enable(); else schedule.Disable();
            if (!taskDetail.Reentrant) _ = schedule.NonReentrant();
        }
        return (taskDetail, (Action<Schedule>)scheduleAction);
    }
}
