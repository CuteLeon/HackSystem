using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Infrastructure;

public class IntermediaryNotificationPublisher : IIntermediaryNotificationPublisher
{
    private readonly ILogger<IntermediaryNotificationPublisher> logger;
    private readonly IPublisher publisher;

    public IntermediaryNotificationPublisher(
        ILogger<IntermediaryNotificationPublisher> logger,
        IPublisher publisher)
    {
        this.logger = logger;
        this.publisher = publisher;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : IIntermediaryNotification
    {
        this.logger.LogDebug($"Publishing notification type of {typeof(TNotification).FullName}...");
        await this.publisher.Publish(notification, cancellationToken);
    }
}
