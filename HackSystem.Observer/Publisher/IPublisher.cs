using HackSystem.Observer.Message;

namespace HackSystem.Observer.Publisher;

[Obsolete]
public interface IPublisher<TMessage> : IObservable<TMessage>, IDisposable
    where TMessage : MessageBase
{
    [Obsolete]
    void UnSubsciber(IObserver<TMessage> subscriber);

    [Obsolete]
    Task Publish(TMessage message);
}
