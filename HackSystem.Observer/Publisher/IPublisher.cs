using System;
using System.Threading.Tasks;
using HackSystem.Observer.Subscriber;

namespace HackSystem.Observer.Publisher
{
    public interface IPublisher<TMessage> : IDisposable
    {
        void AddSubsciber(ISubscriber<TMessage> subscriber);

        void RemoveSubsciber(ISubscriber<TMessage> subscriber);

        Task Publish(TMessage message);
    }
}
