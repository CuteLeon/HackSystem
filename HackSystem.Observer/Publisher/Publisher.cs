using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.Observer.Subscriber;

namespace HackSystem.Observer.Publisher
{
    public class Publisher<TMessage> : IPublisher<TMessage>
    {
        protected HashSet<ISubscriber<TMessage>> Subscribers = new HashSet<ISubscriber<TMessage>>();

        public void AddSubsciber(ISubscriber<TMessage> subscriber)
        {
            this.Subscribers.Add(subscriber);
        }

        public void RemoveSubsciber(ISubscriber<TMessage> subscriber)
        {
            this.Subscribers.Remove(subscriber);
        }

        public async Task Publish(TMessage message)
        {
            if (!this.Subscribers.Any()) return;

            foreach (var subscriber in this.Subscribers)
            {
                _ = subscriber.ReceiveMessage(message);
            }

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            this.Subscribers.Clear();
        }
    }
}
