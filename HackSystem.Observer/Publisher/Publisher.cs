using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using HackSystem.Observer.Message;
using Microsoft.Extensions.Logging;

namespace HackSystem.Observer.Publisher;

    public class Publisher<TMessage> : IPublisher<TMessage>
        where TMessage : MessageBase
    {
        protected BufferBlock<TMessage> messageBufferBlock;
        protected Dictionary<IObserver<TMessage>, IDisposable> observers;
        protected readonly IObservable<TMessage> observable;
        protected readonly ILogger<IPublisher<TMessage>> logger;
        protected readonly string messageType = typeof(TMessage).Name;

        public Publisher(ILogger<IPublisher<TMessage>> logger)
        {
            this.logger = logger;
            this.messageBufferBlock = new BufferBlock<TMessage>();
            this.observers = new Dictionary<IObserver<TMessage>, IDisposable>();
            this.observable = this.messageBufferBlock.AsObservable();
        }

        public IDisposable Subscribe(IObserver<TMessage> observer)
        {
            if (this.observers.ContainsKey(observer))
            {
                throw new InvalidOperationException($"{messageType} observer ({observer.GetHashCode():X}) has been subscribed.");
            }

            var disposable = this.observable.Subscribe(observer);
            this.observers.Add(observer, disposable);
            this.logger.LogInformation($"Publisher of {this.messageType}, observer ({observer.GetHashCode():X}) subscribed.");
            return disposable;
        }

        public void UnSubsciber(IObserver<TMessage> observer)
        {
            if (!this.observers.TryGetValue(observer, out var disposable))
            {
                throw new InvalidOperationException($"{messageType} observer ({observer.GetHashCode():X}) has not been subscribed.");
            }

            // Unsubscribe automatically when dispose.
            disposable.Dispose();
            this.observers.Remove(observer);
            this.logger.LogInformation($"Publisher of {this.messageType}, observer ({observer.GetHashCode():X}) unsubscribed.");
        }

        public async Task Publish(TMessage message)
        {
            this.logger.LogInformation($"Publisher of {this.messageType}, publish message: {message}.");
            if (!this.observers.Any()) return;

            foreach (var subscriber in this.observers.Keys)
            {
                subscriber.OnNext(message);
            }

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            foreach (var subscriber in this.observers.Keys)
            {
                this.UnSubsciber(subscriber);
            }
            GC.SuppressFinalize(this);
        }
    }
