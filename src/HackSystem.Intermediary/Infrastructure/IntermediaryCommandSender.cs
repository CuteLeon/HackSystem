using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Infrastructure;

public class IntermediaryCommandSender : IIntermediaryCommandSender
{
    private readonly ILogger<IntermediaryCommandSender> logger;
    private readonly IMediator mediator;

    public IntermediaryCommandSender(
        ILogger<IntermediaryCommandSender> logger,
        IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Send(IIntermediaryCommand command, CancellationToken cancellationToken = default)
    {
        this.logger.LogDebug($"Sending command of type {command?.GetType()?.FullName ?? "[Null]"}...");
        _ = await this.mediator.Send(command, cancellationToken);
    }
}
