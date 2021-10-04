using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Extensions;

public static class HackSystemIntermediaryHandlerExtension
{
    public static IServiceCollection AddIntermediaryNotificationHandler<TNotificationHandler, TNotification>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TNotificationHandler : IIntermediaryNotificationHandler<TNotification>
        where TNotification : IIntermediaryNotification
    {
        services.Add(new ServiceDescriptor(typeof(INotificationHandler<TNotification>), typeof(TNotificationHandler), lifetime));
        return services;
    }

    public static IServiceCollection AddIntermediaryCommandHandler<TCommandHandler, TCommand>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandHandler : IIntermediaryRequestHandler<TCommand, ValueTuple>
        where TCommand : IIntermediaryRequest<ValueTuple>
        => services.AddIntermediaryRequestHandler<TCommandHandler, TCommand, ValueTuple>(lifetime);

    public static IServiceCollection AddIntermediaryRequestHandler<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestHandler : IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.Add(new ServiceDescriptor(typeof(IRequestHandler<TRequest, TResponse>), typeof(TRequestHandler), lifetime));
        return services;
    }
}
