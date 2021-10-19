using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using Xunit;

namespace HackSystem.Intermediary.Infrastructure.Tests;

public class IntermediaryEventPublisherTests
{
    class TestEvent : IIntermediaryEvent { }

    [Fact()]
    public async Task PublishTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryEvent<TestEvent>();
        IServiceProvider provider = services.BuildServiceProvider();
        var publisher = provider.GetRequiredService<IIntermediaryEventPublisher>();
        var eventHandler = provider.GetRequiredService<IIntermediaryEventHandler<TestEvent>>();
        var count = 0;
        eventHandler.EventRaised += (s, e) => count++;
        for (int index = 0; index < 10; index++)
        {
            await publisher.Publish(new TestEvent());
        }

        Assert.Equal(10, count);
    }

    [Fact()]
    public void AddIntermediaryEventTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddIntermediaryEvent<TestEvent>();
        IServiceProvider provider = services.BuildServiceProvider();
        Assert.Same(
            provider.GetRequiredService<IIntermediaryEventHandler<TestEvent>>(),
            provider.GetRequiredService<IIntermediaryEventHandler<TestEvent>>());
        Assert.NotNull(provider.GetRequiredService<IIntermediaryEventHandler<TestEvent>>());
    }
}
