using System;
using System.Threading.Tasks;

namespace HackSystem.Observer.Subscriber
{
    public interface ISubscriber<TMessage> : IDisposable
    {
        void Subscibe();

        void Unsubscibe();

        Func<TMessage, Task> HandleMessage { get; set; }

        Task ReceiveMessage(TMessage message);
    }
}
