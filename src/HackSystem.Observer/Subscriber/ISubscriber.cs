using HackSystem.Observer.Message;

namespace HackSystem.Observer.Subscriber;

[Obsolete]
public interface ISubscriber<TMessage> : IObserver<TMessage>, IDisposable
    where TMessage : MessageBase
{
    [Obsolete]
    void Subscibe();

    [Obsolete]
    void Unsubscibe();

    [Obsolete]
    Func<TMessage, Task> HandleMessage { get; set; }
}
