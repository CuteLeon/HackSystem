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

    public static IServiceCollection AddIntermediaryNotificationHandlerSingleton<TNotificationHandler, TNotification>(
        this IServiceCollection services,
        IIntermediaryNotificationHandler<TNotification> singletonInstance)
        where TNotificationHandler : IIntermediaryNotificationHandler<TNotification>
        where TNotification : IIntermediaryNotification
    {
        services.AddSingleton<INotificationHandler<TNotification>>(singletonInstance);
        return services;
    }

    public static IServiceCollection AddIntermediaryCommandHandlerSingleton<TCommandHandler, TCommand>(
        this IServiceCollection services,
        IIntermediaryRequestHandler<TCommand, ValueTuple> singletonInstance)
        where TCommandHandler : IIntermediaryRequestHandler<TCommand, ValueTuple>
        where TCommand : IIntermediaryRequest<ValueTuple>
        => services.AddIntermediaryRequestHandlerSingleton<TCommandHandler, TCommand, ValueTuple>(singletonInstance);

    public static IServiceCollection AddIntermediaryRequestHandlerSingleton<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        IIntermediaryRequestHandler<TRequest, TResponse> singletonInstance)
        where TRequestHandler : IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.AddSingleton<IRequestHandler<TRequest, TResponse>>(singletonInstance);
        return services;
    }
}
