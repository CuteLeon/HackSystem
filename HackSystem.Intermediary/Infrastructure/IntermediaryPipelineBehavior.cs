namespace HackSystem.Intermediary.Infrastructure;

public class IntermediaryPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<IntermediaryPipelineBehavior<TRequest, TResponse>> logger;

    public IntermediaryPipelineBehavior(
        ILogger<IntermediaryPipelineBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        this.logger.LogDebug($"Handling request of type {request.GetType().FullName}...");
        var response = await next.Invoke();
        this.logger.LogDebug($"Handled with response of type {response.GetType().FullName}...");
        return response;
    }
}
