using HackSystem.Observer.Publisher;
using HackSystem.Observer.Subscriber;

namespace HackSystem.Observer;

public static class HackSystemObserverExtension
{
    public static IServiceCollection AddHackSystemObserver(this IServiceCollection services)
    {
        services
            .AddSingleton(typeof(IPublisher<>), typeof(Publisher<>))
            .AddScoped(typeof(ISubscriber<>), typeof(Subscriber<>));

        return services;
    }
}
