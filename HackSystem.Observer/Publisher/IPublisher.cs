using System;
using System.Threading.Tasks;
using HackSystem.Observer.Message;

namespace HackSystem.Observer.Publisher
{
    public interface IPublisher<TMessage> : IObservable<TMessage>, IDisposable
        where TMessage : MessageBase
    {
        void UnSubsciber(IObserver<TMessage> subscriber);

        Task Publish(TMessage message);
    }
}
