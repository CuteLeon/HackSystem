using System;
using System.Threading.Tasks;
using HackSystem.Observer.Publisher;

namespace HackSystem.Observer.Subscriber
{
    public class Subscriber<TMessage> : ISubscriber<TMessage>
    {
        private readonly IPublisher<TMessage> publisher;

        public Func<TMessage, Task> HandleMessage { get; set; }

        public Subscriber(IPublisher<TMessage> publisher)
        {
            this.publisher = publisher;

            this.Subscibe();
        }

        public async Task ReceiveMessage(TMessage message)
        {
            await this.HandleMessage?.Invoke(message);
        }

        public void Subscibe()
        {
            this.publisher?.AddSubsciber(this);
        }

        public void Unsubscibe()
        {
            this.publisher?.RemoveSubsciber(this);
        }

        public void Dispose()
        {
            this.HandleMessage = null;
            this.Unsubscibe();
        }
    }
}
