using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryNotificationPublisher
{
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : IIntermediaryNotification;
}
