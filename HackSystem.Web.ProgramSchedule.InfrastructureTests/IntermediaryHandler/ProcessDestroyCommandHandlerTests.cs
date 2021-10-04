using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Extensions;
using HackSystem.Web.ProgramSchedule.Application.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler.Tests;

public class ProcessDestroyCommandHandlerTests
{
    [Fact()]
    public async Task ProcessDestroyCommandHandlerTest()
    {
        var commandCount = 0;
        var commandHandler = new Mock<IProcessDestroyCommandHandler>();
        commandHandler
            .Setup(x => x.Handle(It.IsAny<ProcessDestroyCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ValueTuple.Create())
            .Callback(() => commandCount++);
        IServiceCollection serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryCommandHandlerSingleton<ProcessDestroyCommandHandler, ProcessDestroyCommand>(commandHandler.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var commandSender = serviceProvider.GetRequiredService<IIntermediaryCommandSender>();
        for (int index = 0; index < 5; index++)
        {
            await commandSender.Send(new ProcessDestroyCommand() { ProcessDetail = new ProcessDetail { PID = index } });
        }
        Assert.Equal(5, commandCount);
    }
}
