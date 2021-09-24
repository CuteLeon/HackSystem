namespace HackSystem.Intermediary.Domain;

public interface IIntermediaryRequest<out TResponse> : IRequest<TResponse>
{
}

// ValueTuple insteads of obsolete System.Void
public interface IIntermediaryRequest : IIntermediaryRequest<ValueTuple>
{
}
