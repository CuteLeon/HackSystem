using HackSystem.Observer.Publisher;
using HackSystem.Observer.Subscriber;

namespace HackSystem.Observer;

[Obsolete]
public static class HackSystemObserverExtension
{
    [Obsolete]
    public static IServiceCollection AddHackSystemObserver(this IServiceCollection services)
    {
        services
            .AddSingleton(typeof(IPublisher<>), typeof(Publisher<>))
            .AddScoped(typeof(ISubscriber<>), typeof(Subscriber<>));

        return services;
    }
}
