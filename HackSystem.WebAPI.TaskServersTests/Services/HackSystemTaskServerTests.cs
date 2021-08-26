using System;
using System.Linq;
using FluentScheduler;
using HackSystem.WebAPI.Model.Task;
using HackSystem.WebAPI.TaskServers.Configurations;
using HackSystem.WebAPI.TaskServers.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace HackSystem.WebAPI.TaskServers.Services.Tests;

public class HackSystemTaskServerTests
{
    [Fact]
    public void LoadUnloadTasksTest()
    {
        var taskDetails = Enumerable.Range(1, 5).Select(index => new TaskDetail() { TaskName = $"Task_{index}" }).ToList();
        var options = new TaskServerOptions();
        var mockLogger = new Mock<ILogger<HackSystemTaskServer>>();
        var mockOptions = new Mock<IOptionsMonitor<TaskServerOptions>>();
        mockOptions.SetupGet(m => m.CurrentValue).Returns(options);
        var mockTaskLoader = new Mock<ITaskLoader>();
        mockTaskLoader.Setup(l => l.GetTaskDetails()).Returns(taskDetails);
        var mockTaskScheduleWrapper = new Mock<ITaskScheduleWrapper>();
        mockTaskScheduleWrapper.Setup(w => w.WrapTaskSchedule(It.IsAny<TaskDetail>())).Returns(new Func<TaskDetail, (TaskDetail TaskDetail, Action<Schedule> ScheduleAction)>(
            taskDetail => (taskDetail, new Action<Schedule>(s => s.WithName(taskDetail.TaskName).ToRunOnceIn(1).Days()))));
        var mockTaskGenericJob = new Mock<ITaskGenericJob>();
        mockTaskGenericJob.Setup(j => j.Execute()).Verifiable();
        var mockServiceProvider = new Mock<IServiceProvider>();
        mockServiceProvider.Setup(p => p.GetService(It.Is<Type>(type => type == typeof(ITaskLoader)))).Returns(mockTaskLoader.Object);
        mockServiceProvider.Setup(p => p.GetService(It.Is<Type>(type => type == typeof(ITaskScheduleWrapper)))).Returns(mockTaskScheduleWrapper.Object);
        mockServiceProvider.Setup(p => p.GetService(It.Is<Type>(type => type == typeof(ITaskGenericJob)))).Returns(mockTaskGenericJob.Object);
        var mockServiceScope = new Mock<IServiceScope>();
        mockServiceScope.SetupGet(s => s.ServiceProvider).Returns(mockServiceProvider.Object);
        var mockServiceScopeFactory = new Mock<IServiceScopeFactory>();
        mockServiceScopeFactory.Setup(s => s.CreateScope()).Returns(mockServiceScope.Object);

        var hackSystemTaskServer = new HackSystemTaskServer(
            mockLogger.Object,
            mockOptions.Object,
            mockServiceScopeFactory.Object);
        hackSystemTaskServer.LoadTasks();
        Assert.Equal(taskDetails.Count(), JobManager.AllSchedules.Count());

        hackSystemTaskServer.UnloadTask(new TaskDetail { TaskName = "TempTask" });
        Assert.Equal(taskDetails.Count(), JobManager.AllSchedules.Count());

        hackSystemTaskServer.LoadTask(new TaskDetail { TaskName = "TempTask" });
        Assert.Equal(taskDetails.Count() + 1, JobManager.AllSchedules.Count());

        hackSystemTaskServer.UnloadTask(new TaskDetail { TaskName = "TempTask" });
        Assert.Equal(taskDetails.Count(), JobManager.AllSchedules.Count());

        hackSystemTaskServer.UnloadTasks();
        Assert.Empty(JobManager.AllSchedules);
    }
}
