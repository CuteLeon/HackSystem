using System;
using System.Threading.Tasks;
using HackSystem.Observer.Message;

namespace HackSystem.Observer.Subscriber
{
    public interface ISubscriber<TMessage> : IObserver<TMessage>, IDisposable
        where TMessage : MessageBase
    {
        void Subscibe();

        void Unsubscibe();

        Func<TMessage, Task> HandleMessage { get; set; }
    }
}
