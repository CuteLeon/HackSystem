using HackSystem.Intermediary.Extensions;
using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Domain.Configuration;
using HackSystem.WebAPI.Domain.Notifications;
using HackSystem.WebAPI.Infrastructure.DBContexts;
using HackSystem.WebAPI.Infrastructure.Middleware;
using HackSystem.WebAPI.Infrastructure.NotificationHandlers;
using HackSystem.WebAPI.Infrastructure.Repository;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IO;

namespace HackSystem.WebAPI.Extensions;

public static class HackSystemInfrastructureExtension
{
    public static IServiceCollection AddHackSystemWebAPIServices(
        this IServiceCollection services)
    {
        services
            .AddHackSystemNotificationHandler<CreateAccountNotificationHandler, CreateAccountNotification>()
            .AddScoped<IGenericOptionRepository, GenericOptionRepository>()
            .AddScoped<IWebAPILogRepository, WebAPILogRepository>()
            .AddScoped<IProgramUserRepository, ProgramUserRepository>()
            .AddScoped<RecyclableMemoryStreamManager>();

        return services;
    }

    public static IServiceCollection AddHackSystemDbContext(
        this IServiceCollection services,
        HackSystemDbContextOptions dbContextOptions)
    {
        services
            .AddDbContext<DbContext, HackSystemDbContext>(
                options => options
                    .UseSqlite(dbContextOptions.ConnectionString)
                    .UseLazyLoadingProxies(),
                ServiceLifetime.Scoped);

        return services;
    }

    public static IApplicationBuilder UseHackSystemWebAPILogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<WebAPILoggingMiddleware>();
        return app;
    }
}
