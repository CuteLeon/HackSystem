using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IIntermediaryEvent
{
    event EventHandler<TEvent> EventRaised;
}
