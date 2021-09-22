using HackSystem.WebAPI.Application.Behaviors;
using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Infrastructure.Behaviors;
using HackSystem.WebAPI.Infrastructure.Middleware;
using HackSystem.WebAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.IO;

namespace HackSystem.WebAPI.Infrastructure.Extensions;

public static class HackSystemInfrastructureExtension
{
    public static IServiceCollection AddWebAPIServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAccountCreatedNotificationHandler, AccountCreatedNotificationHandler>()
            .AddScoped<IGenericOptionRepository, GenericOptionRepository>()
            .AddScoped<IWebAPILogRepository, WebAPILogRepository>()
            .AddScoped<RecyclableMemoryStreamManager>();
        return services;
    }

    public static IApplicationBuilder UseWebAPILogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<WebAPILoggingMiddleware>();
        return app;
    }
}
