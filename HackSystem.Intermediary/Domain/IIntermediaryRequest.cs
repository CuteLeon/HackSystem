namespace HackSystem.Intermediary.Domain;

public interface IIntermediaryRequest<out TResponse> : IRequest<TResponse>
{
}
