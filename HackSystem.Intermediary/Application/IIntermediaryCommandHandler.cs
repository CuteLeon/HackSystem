using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application;

public interface IIntermediaryCommandHandler<TCommand> : IIntermediaryRequestHandler<TCommand, ValueTuple>
    where TCommand : IIntermediaryCommand
{
}
