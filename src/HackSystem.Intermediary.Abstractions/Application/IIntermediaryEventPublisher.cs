using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryEventPublisher
{
    Task Publish<TEvent>(TEvent eventArg, CancellationToken cancellationToken = default)
        where TEvent : IIntermediaryEvent;
}
