using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Entity;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Container.Tests;

public class ProcessContainerTests
{
    [Fact()]
    public void ProcessContainerTest()
    {
        IProcessContainer container = new ProcessContainer(new Mock<ILogger<ProcessContainer>>().Object);
        for (int index = 0; index < 5; index++)
        {
            container.LaunchProcess(new ProcessDetail(index, default));
            Assert.NotNull(container.GetProcess(index));
        }
        Assert.Equal(5, container.GetProcesses().Count());

        Assert.True(container.DestroyProcess(2, out var process));
        Assert.NotNull(process);

        Assert.True(container.DestroyProcess(4, out process));
        Assert.NotNull(process);

        Assert.False(container.DestroyProcess(6, out process));
        Assert.Null(process);
    }
}
