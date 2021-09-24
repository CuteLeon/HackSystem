using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Infrastructure;

public class IntermediaryRequestSender : IIntermediaryRequestSender
{
    private readonly ILogger<IntermediaryRequestSender> logger;
    private readonly IMediator mediator;

    public IntermediaryRequestSender(
        ILogger<IntermediaryRequestSender> logger,
        IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task<TResponse> Send<TResponse>(IIntermediaryRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        this.logger.LogDebug($"Sending request of type {request?.GetType()?.FullName ?? "[Null]"}...");
        var response = await this.mediator.Send(request, cancellationToken);
        this.logger.LogDebug($"Received response of type {response?.GetType()?.FullName ?? "[Null]"}.");
        return response;
    }
}
