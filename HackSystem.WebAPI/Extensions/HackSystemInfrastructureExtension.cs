using HackSystem.WebAPI.Application.Behaviors;
using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Domain.Configuration;
using HackSystem.WebAPI.Infrastructure.Behaviors;
using HackSystem.WebAPI.Infrastructure.DBContexts;
using HackSystem.WebAPI.Infrastructure.Middleware;
using HackSystem.WebAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IO;

namespace HackSystem.WebAPI.Extensions;

public static class HackSystemInfrastructureExtension
{
    public static IServiceCollection AddHackSystemWebAPIServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAccountCreatedNotificationHandler, AccountCreatedNotificationHandler>()
            .AddScoped<IGenericOptionRepository, GenericOptionRepository>()
            .AddScoped<IWebAPILogRepository, WebAPILogRepository>()
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
