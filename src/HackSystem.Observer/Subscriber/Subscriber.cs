using HackSystem.Observer.Message;
using HackSystem.Observer.Publisher;

namespace HackSystem.Observer.Subscriber;

[Obsolete]
public class Subscriber<TMessage> : ISubscriber<TMessage>
    where TMessage : MessageBase
{
    private readonly ILogger<ISubscriber<TMessage>> logger;
    private readonly IPublisher<TMessage> publisher;
    protected readonly string messageType = typeof(TMessage).Name;

    [Obsolete]
    public Func<TMessage, Task> HandleMessage { get; set; }

    [Obsolete]
    public Subscriber(
        ILogger<ISubscriber<TMessage>> logger,
        IPublisher<TMessage> publisher)
    {
        this.logger = logger;
        this.publisher = publisher;

        this.Subscibe();
    }

    [Obsolete]
    public void Subscibe()
    {
        this.publisher?.Subscribe(this);
    }

    [Obsolete]
    public void Unsubscibe()
    {
        this.publisher?.UnSubsciber(this);
    }

    [Obsolete]
    public void Dispose()
    {
        this.HandleMessage = null;
        this.Unsubscibe();
        GC.SuppressFinalize(this);
    }

    [Obsolete]
    public void OnCompleted()
    {
        this.logger.LogInformation($"Subscriber of {this.messageType}, completed.");
    }

    [Obsolete]
    public void OnError(Exception error)
    {
        this.logger.LogInformation($"Subscriber of {this.messageType}, exception: {error.Message}");
    }

    [Obsolete]
    public void OnNext(TMessage message)
    {
        this.logger.LogInformation($"Subscriber of {this.messageType}, received message: {message}");
        this.HandleMessage?.Invoke(message);
    }
}
