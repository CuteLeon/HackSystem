using System.Diagnostics;
using FluentScheduler;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using Xunit;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.ScheduleWrappers.Tests;

public class TaskScheduleWrapperTests
{
    [Theory]
    [InlineData("Task_Name_1", true, 1, 1, 1, 0, 0, 0, 0, 0, TaskFrequency.Automatically, false)]
    [InlineData("Task_Name_2", true, 1, 1, 1, 0, 0, 0, 0, 0, TaskFrequency.Automatically, false)]
    [InlineData("Task_Name_3", true, 1, 1, 1, 0, 0, 0, 0, 0, TaskFrequency.Manually, true)]
    [InlineData("Task_Name_4", true, 1, 1, 1, 0, 0, 0, 0, 0, TaskFrequency.Manually, false)]
    [InlineData("Task_Name_5", true, 1, 1, 1, 0, 0, 0, 0, 300, TaskFrequency.Automatically, false)]
    [InlineData("Task_Name_6", true, 1, 1, 1, 0, 0, 0, 120, 300, TaskFrequency.Automatically, false)]
    [InlineData("Task_Name_7", true, 2021, 7, 20, 22, 36, 55, 120, 300, TaskFrequency.Automatically, false)]
    [InlineData("Task_Name_8", true, 1, 1, 1, 0, 0, 0, 0, 0, TaskFrequency.Once, false)]
    [InlineData("Task_Name_9", true, 1, 1, 1, 0, 0, 0, 300, 0, TaskFrequency.Once, false)]
    [InlineData("Task_Name_10", true, 1, 5, 15, 14, 36, 55, 0, 0, TaskFrequency.Once, false)]
    [InlineData("Task_Name_11", true, 9999, 5, 15, 14, 36, 55, 0, 0, TaskFrequency.Once, false)]
    [InlineData("Task_Name_12", true, 1, 1, 1, 23, 59, 59, 0, 0, TaskFrequency.Daily, false)]
    [InlineData("Task_Name_13", true, 1, 1, 1, 0, 0, 1, 0, 0, TaskFrequency.Daily, false)]
    [InlineData("Task_Name_14", true, 1, 1, 1, 23, 59, 59, 0, 0, TaskFrequency.Daily, false)]
    [InlineData("Task_Name_15", true, 1, 1, 1, 0, 0, 1, 0, 5 * 24 * 3600, TaskFrequency.Daily, false)]
    [InlineData("Task_Name_16", true, 2021, 7, 14, 14, 36, 55, 0, 0, TaskFrequency.Weekly, false)]
    [InlineData("Task_Name_17", true, 2021, 7, 14, 14, 36, 55, 0, 3 * 7 * 24 * 3600, TaskFrequency.Weekly, false)]
    [InlineData("Task_Name_18", true, 2021, 1, 1, 14, 36, 55, 0, 0, TaskFrequency.Monthly, false)]
    [InlineData("Task_Name_19", true, 2021, 12, 31, 14, 36, 55, 0, 0, TaskFrequency.Monthly, false)]
    public void WrapTaskScheduleTest(
        string taskName,
        bool enabled,
        int year, int month, int day, int hour, int minute, int second,
        int firstInterval,
        int automaticInterval,
        TaskFrequency taskFrequency,
        bool reentrant)
    {
        var taskDetail = new TaskDetail
        {
            TaskName = taskName,
            Enabled = enabled,
            ExecuteDateTime = new DateTime(year, month, day, hour, minute, second),
            FirstInterval = new TimeSpan(0, 0, firstInterval),
            AutomaticInterval = new TimeSpan(0, 0, automaticInterval),
            TaskFrequency = taskFrequency,
            Reentrant = reentrant
        };
        var taskScheduleWrapper = new TaskScheduleWrapper();
        var taskSchedule = taskScheduleWrapper.WrapTaskSchedule(taskDetail);

        JobManager.AddJob(this.Job, taskSchedule.ScheduleAction);
        if ((taskFrequency == TaskFrequency.Manually) ||
            (taskFrequency == TaskFrequency.Once &&
             ((taskDetail.ExecuteDateTime != default && taskDetail.ExecuteDateTime < DateTime.Now) ||
              (taskDetail.ExecuteDateTime == default && taskDetail.FirstInterval == default))) ||
            (taskFrequency == TaskFrequency.Monthly && taskDetail.ExecuteDateTime.Day <= DateTime.Now.Day))
            return;

        Assert.Single(JobManager.AllSchedules);
        var nextRun = JobManager.AllSchedules.First().NextRun;
        JobManager.RemoveAllJobs();
    }

    private void Job()
    {
        Debug.WriteLine($"Job runs at {DateTime.Now}");
    }
}
