using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryNotificationHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IIntermediaryNotification
{
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}
