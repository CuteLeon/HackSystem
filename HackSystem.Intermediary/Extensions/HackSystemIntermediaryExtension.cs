using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Infrastructure;

namespace HackSystem.Intermediary.Extensions;

public static class HackSystemIntermediaryExtension
{
    public static IServiceCollection AddHackSystemIntermediary(this IServiceCollection services)
        => services
            .AddMediatR(typeof(HackSystemIntermediaryExtension).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(IntermediaryPipelineBehavior<,>))
            .AddTransient<IIntermediaryNotificationPublisher, IntermediaryNotificationPublisher>()
            .AddTransient<IIntermediaryRequestSender, IntermediaryRequestSender>();

    public static IServiceCollection AddHackSystemNotificationHandler<TNotificationHandler, TNotification>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TNotificationHandler : IIntermediaryNotificationHandler<TNotification>
        where TNotification : IIntermediaryNotification
    {
        services.Add(new ServiceDescriptor(typeof(INotificationHandler<TNotification>), typeof(TNotificationHandler), lifetime));
        return services;
    }

    public static IServiceCollection AddHackSystemRequestHandler<TRequestHandler, TRequest>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TRequestHandler : IIntermediaryRequestHandler<TRequest, ValueTuple>
        where TRequest : IIntermediaryRequest<ValueTuple>
    {
        services.Add(new ServiceDescriptor(typeof(IRequestHandler<TRequest, ValueTuple>), typeof(TRequestHandler), lifetime));
        return services;
    }

    public static IServiceCollection AddHackSystemRequestHandler<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        ServiceLifetime lifetime)
        where TRequestHandler : IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.Add(new ServiceDescriptor(typeof(IRequestHandler<TRequest, TResponse>), typeof(TRequestHandler), lifetime));
        return services;
    }
}
