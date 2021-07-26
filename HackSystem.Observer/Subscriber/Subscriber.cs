using System;
using System.Threading.Tasks;
using HackSystem.Observer.Message;
using HackSystem.Observer.Publisher;
using Microsoft.Extensions.Logging;

namespace HackSystem.Observer.Subscriber
{
    public class Subscriber<TMessage> : ISubscriber<TMessage>
        where TMessage : MessageBase
    {
        private readonly ILogger<ISubscriber<TMessage>> logger;
        private readonly IPublisher<TMessage> publisher;
        protected readonly string messageType = typeof(TMessage).Name;

        public Func<TMessage, Task> HandleMessage { get; set; }

        public Subscriber(
            ILogger<ISubscriber<TMessage>> logger,
            IPublisher<TMessage> publisher)
        {
            this.logger = logger;
            this.publisher = publisher;

            this.Subscibe();
        }

        public void Subscibe()
        {
            this.publisher?.Subscribe(this);
        }

        public void Unsubscibe()
        {
            this.publisher?.UnSubsciber(this);
        }

        public void Dispose()
        {
            this.HandleMessage = null;
            this.Unsubscibe();
            GC.SuppressFinalize(this);
        }

        public void OnCompleted()
        {
            this.logger.LogInformation($"Subscriber of {this.messageType}, completed.");
        }

        public void OnError(Exception error)
        {
            this.logger.LogInformation($"Subscriber of {this.messageType}, exception: {error.Message}");
        }

        public void OnNext(TMessage message)
        {
            this.logger.LogInformation($"Subscriber of {this.messageType}, received message: {message}");
            this.HandleMessage?.Invoke(message);
        }
    }
}
