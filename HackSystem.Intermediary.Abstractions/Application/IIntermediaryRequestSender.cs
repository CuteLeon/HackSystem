using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryRequestSender
{
    Task<TResponse> Send<TResponse>(IIntermediaryRequest<TResponse> request, CancellationToken cancellationToken = default);
}
