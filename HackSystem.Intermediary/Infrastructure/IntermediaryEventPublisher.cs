using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Infrastructure;

public class IntermediaryEventPublisher : IIntermediaryEventPublisher
{
    private readonly ILogger<IntermediaryEventPublisher> logger;
    private readonly IPublisher publisher;

    public IntermediaryEventPublisher(
        ILogger<IntermediaryEventPublisher> logger,
        IPublisher publisher)
    {
        this.logger = logger;
        this.publisher = publisher;
    }

    public async Task Publish<TEvent>(TEvent eventArg, CancellationToken cancellationToken = default)
        where TEvent : IIntermediaryEvent
    {
        this.logger.LogDebug($"Publishing event type of {typeof(TEvent).FullName}...");
        await this.publisher.Publish(eventArg, cancellationToken);
    }
}
