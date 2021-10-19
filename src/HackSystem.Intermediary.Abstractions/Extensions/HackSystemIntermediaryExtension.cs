using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Intermediary.Extensions;

public static class HackSystemIntermediaryHandlerExtension
{
    public static IServiceCollection AddIntermediaryNotificationHandler<TNotificationHandlerService, TNotificationHandlerImplement, TNotification>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TNotificationHandlerService : IIntermediaryNotificationHandler<TNotification>
        where TNotificationHandlerImplement : class, TNotificationHandlerService
        where TNotification : IIntermediaryNotification
    {
        services.Add(new ServiceDescriptor(
            typeof(TNotificationHandlerService),
            sp => sp.GetRequiredService<INotificationHandler<TNotification>>(),
            lifetime));
        services.AddIntermediaryNotificationHandler<TNotificationHandlerImplement, TNotification>(lifetime);
        return services;
    }

    public static IServiceCollection AddIntermediaryCommandHandler<TCommandHandlerService, TCommandHandlerImplement, TCommand>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TCommandHandlerService : IIntermediaryCommandHandler<TCommand>
        where TCommandHandlerImplement : class, TCommandHandlerService
        where TCommand : IIntermediaryCommand
    {
        services.Add(new ServiceDescriptor(
            typeof(TCommandHandlerService),
            sp => sp.GetRequiredService<IRequestHandler<TCommand, ValueTuple>>(),
            lifetime));
        services.AddIntermediaryCommandHandler<TCommandHandlerImplement, TCommand>(lifetime);
        return services.AddIntermediaryRequestHandler<TCommandHandlerService, TCommandHandlerImplement, TCommand, ValueTuple>(lifetime);
    }

    public static IServiceCollection AddIntermediaryRequestHandler<TRequestHandlerService, TRequestHandlerImplement, TRequest, TResponse>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestHandlerService : IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequestHandlerImplement : class, TRequestHandlerService
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.Add(new ServiceDescriptor(
            typeof(TRequestHandlerService),
            sp => sp.GetRequiredService<IRequestHandler<TRequest, TResponse>>(),
            lifetime));
        services.AddIntermediaryRequestHandler<TRequestHandlerImplement, TRequest, TResponse>(lifetime);
        return services;
    }

    public static IServiceCollection AddIntermediaryNotificationHandler<TNotificationHandler, TNotification>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TNotificationHandler : class, IIntermediaryNotificationHandler<TNotification>
        where TNotification : IIntermediaryNotification
    {
        services.Add(new ServiceDescriptor(
            typeof(TNotificationHandler),
            sp => sp.GetRequiredService<INotificationHandler<TNotification>>(),
            lifetime));
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
        where TCommandHandler : class, IIntermediaryCommandHandler<TCommand>
        where TCommand : IIntermediaryCommand
    {
        services.Add(new ServiceDescriptor(
            typeof(TCommandHandler),
            sp => sp.GetRequiredService<IRequestHandler<TCommand, ValueTuple>>(),
            lifetime));
        services.Add(new ServiceDescriptor(
            typeof(IIntermediaryCommandHandler<TCommand>),
            sp => sp.GetRequiredService<IRequestHandler<TCommand, ValueTuple>>(),
            lifetime));
        return services.AddIntermediaryRequestHandler<TCommandHandler, TCommand, ValueTuple>(lifetime);
    }

    public static IServiceCollection AddIntermediaryRequestHandler<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TRequestHandler : class, IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
    {
        services.Add(new ServiceDescriptor(
            typeof(TRequestHandler),
            sp => sp.GetRequiredService<IRequestHandler<TRequest, TResponse>>(),
            lifetime));
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

    public static IServiceCollection AddIntermediaryNotificationHandlerSingleton<TNotificationHandler, TNotification>(
        this IServiceCollection services,
        TNotificationHandler singletonInstance)
        where TNotificationHandler : class, IIntermediaryNotificationHandler<TNotification>
        where TNotification : IIntermediaryNotification
        => services
            .AddSingleton<TNotificationHandler>(singletonInstance)
            .AddSingleton<INotificationHandler<TNotification>>(singletonInstance)
            .AddSingleton<IIntermediaryNotificationHandler<TNotification>>(singletonInstance);

    public static IServiceCollection AddIntermediaryCommandHandlerSingleton<TCommandHandler, TCommand>(
        this IServiceCollection services,
        TCommandHandler singletonInstance)
        where TCommandHandler : class, IIntermediaryCommandHandler<TCommand>
        where TCommand : IIntermediaryCommand
        => services
            .AddSingleton<TCommandHandler>(singletonInstance)
            .AddIntermediaryRequestHandlerSingleton<TCommandHandler, TCommand, ValueTuple>(singletonInstance)
            .AddSingleton<IIntermediaryCommandHandler<TCommand>>(singletonInstance);

    public static IServiceCollection AddIntermediaryRequestHandlerSingleton<TRequestHandler, TRequest, TResponse>(
        this IServiceCollection services,
        TRequestHandler singletonInstance)
        where TRequestHandler : class, IIntermediaryRequestHandler<TRequest, TResponse>
        where TRequest : IIntermediaryRequest<TResponse>
        => services
            .AddSingleton<TRequestHandler>(singletonInstance)
            .AddSingleton<IRequestHandler<TRequest, TResponse>>(singletonInstance)
            .AddSingleton<IIntermediaryRequestHandler<TRequest, TResponse>>(singletonInstance);

    public static IServiceCollection AddIntermediaryEvent<TEvent>(
        this IServiceCollection services)
        where TEvent : IIntermediaryEvent
    {
        services.Add(new ServiceDescriptor(
            typeof(IIntermediaryEventHandler<TEvent>),
            sp => sp.GetRequiredService<INotificationHandler<TEvent>>(),
            ServiceLifetime.Singleton));
        services.Add(new ServiceDescriptor(
            typeof(INotificationHandler<TEvent>),
            typeof(DefaultIntermediaryEventHandler<TEvent>),
            ServiceLifetime.Singleton));
        return services;
    }
}
