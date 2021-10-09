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
        services.Add(new ServiceDescriptor(
            typeof(IIntermediaryNotificationHandler<TNotification>),
            sp => sp.GetRequiredService<INotificationHandler<TNotification>>(),
            lifetime));
        services.Add(new ServiceDescriptor(
            typeof(INotificationHandler<TNotification>),
            typeof(TNotificationHandler),
            lifetime));
        return services;
    }

    public static IServiceCollection AddIntermediaryCommandHandler<TCommandHandler, TCommand>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandHandler : IIntermediaryCommandHandler<TCommand>
        where TCommand : IIntermediaryCommand
    {
        services.Add(new ServiceDescriptor(
            typeof(IIntermediaryCommandHandler<TCommand>),
            sp => sp.GetRequiredService<IRequestHandler<TCommand, ValueTuple>>(),
            lifetime));
        return services.AddIntermediaryRequestHandler<TCommandHandler, TCommand, ValueTuple>(lifetime);
    }

    public static IServiceCollection AddIntermediaryRequestHandler<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestHandler : IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.Add(new ServiceDescriptor(
            typeof(IIntermediaryRequestHandler<TRequest, TResponse>),
            sp => sp.GetRequiredService<IRequestHandler<TRequest, TResponse>>(),
            lifetime));
        services.Add(new ServiceDescriptor(
            typeof(IRequestHandler<TRequest, TResponse>),
            typeof(TRequestHandler),
            lifetime));
        return services;
    }

    public static IServiceCollection AddIntermediaryNotificationHandlerSingleton<TNotification>(
        this IServiceCollection services,
        IIntermediaryNotificationHandler<TNotification> singletonInstance)
        where TNotification : IIntermediaryNotification
        => services
            .AddSingleton<INotificationHandler<TNotification>>(singletonInstance)
            .AddSingleton<IIntermediaryNotificationHandler<TNotification>>(singletonInstance);

    public static IServiceCollection AddIntermediaryCommandHandlerSingleton<TCommand>(
        this IServiceCollection services,
        IIntermediaryCommandHandler<TCommand> singletonInstance)
        where TCommand : IIntermediaryCommand
        => services
            .AddIntermediaryRequestHandlerSingleton(singletonInstance)
            .AddSingleton<IIntermediaryCommandHandler<TCommand>>(singletonInstance);

    public static IServiceCollection AddIntermediaryRequestHandlerSingleton<TRequest, TResponse>(
        this IServiceCollection services,
        IIntermediaryRequestHandler<TRequest, TResponse> singletonInstance)
        where TRequest : IIntermediaryRequest<TResponse>
        => services
            .AddSingleton<IRequestHandler<TRequest, TResponse>>(singletonInstance)
            .AddSingleton<IIntermediaryRequestHandler<TRequest, TResponse>>(singletonInstance);
}
