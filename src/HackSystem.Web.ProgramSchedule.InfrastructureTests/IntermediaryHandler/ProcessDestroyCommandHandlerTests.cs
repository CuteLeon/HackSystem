﻿using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Extensions;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler.Tests;

public class ProcessDestroyCommandHandlerTests
{
    [Fact()]
    public async Task ProcessDestroyCommandHandlerTest()
    {
        var commandCount = 0;
        var commandHandler = new Mock<IIntermediaryCommandHandler<ProcessDestroyCommand>>();
        commandHandler
            .Setup(x => x.Handle(It.IsAny<ProcessDestroyCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ValueTuple.Create())
            .Callback(() => commandCount++);
        IServiceCollection serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryCommandHandlerSingleton<IIntermediaryCommandHandler<ProcessDestroyCommand>, ProcessDestroyCommand>(commandHandler.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var commandSender = serviceProvider.GetRequiredService<IIntermediaryPublisher>();
        for (int index = 0; index < 5; index++)
        {
            await commandSender.SendCommand(new ProcessDestroyCommand(new ProcessDetail(index, default)));
        }
        Assert.Equal(5, commandCount);
    }
}
