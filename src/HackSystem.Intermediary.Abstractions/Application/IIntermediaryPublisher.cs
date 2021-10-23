using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application
{
    public interface IIntermediaryPublisher
    {
        Task SendCommand(IIntermediaryCommand command, CancellationToken cancellationToken = default);

        Task<TResponse> SendRequest<TResponse>(IIntermediaryRequest<TResponse> request, CancellationToken cancellationToken = default);

        Task PublishNotification(IIntermediaryNotification notification, CancellationToken cancellationToken = default);

        Task PublishEvent(IIntermediaryEvent eventArg, CancellationToken cancellationToken = default);
    }
}
