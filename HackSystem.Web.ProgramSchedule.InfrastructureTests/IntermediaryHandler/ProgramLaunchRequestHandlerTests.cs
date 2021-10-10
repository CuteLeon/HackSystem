using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Extensions;
using HackSystem.Web.ProgramSchedule.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler.Tests;

public class ProgramLaunchRequestHandlerTests
{
    [Fact()]
    public async Task ProgramLaunchRequestHandlerTest()
    {
        var requestCount = 0;
        var requestHandler = new Mock<IProgramLaunchRequestHandler>();
        requestHandler
            .Setup(x => x.Handle(It.IsAny<ProgramLaunchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProgramLaunchResponse())
            .Callback(() => requestCount++);
        IServiceCollection serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryRequestHandlerSingleton(requestHandler.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var requestSender = serviceProvider.GetRequiredService<IIntermediaryRequestSender>();
        for (int index = 0; index < 5; index++)
        {
            _ = await requestSender.Send(new ProgramLaunchRequest() { ProgramDetail = new ProgramDetail() });
        }
        Assert.Equal(5, requestCount);
    }
}
