using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Extensions;
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
        var requestHandler = new Mock<IIntermediaryRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>>();
        requestHandler
            .Setup(x => x.Handle(It.IsAny<ProgramLaunchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ProgramLaunchResponse(default))
            .Callback(() => requestCount++);
        IServiceCollection serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryRequestHandlerSingleton(requestHandler.Object);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var requestSender = serviceProvider.GetRequiredService<IIntermediaryRequestSender>();
        for (int index = 0; index < 5; index++)
        {
            _ = await requestSender.Send(new ProgramLaunchRequest(new ProgramDetail(default, default, default, default, default, default, default, default, default)));
        }
        Assert.Equal(5, requestCount);
    }
}
