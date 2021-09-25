using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Application
{
    public interface IIntermediaryCommandSender
    {
        Task Send(IIntermediaryCommand command, CancellationToken cancellationToken = default);
    }
}
