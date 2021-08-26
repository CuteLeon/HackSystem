using HackSystem.WebAPI.Extensions.WebAPILogs.DataServices;
using HackSystem.WebAPI.Extensions.WebAPILogs.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IO;

namespace HackSystem.WebAPI.Extensions;

public static class HackSystemExtensions
{
    public static IApplicationBuilder UseWebAPILogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<WebAPILoggingMiddleware>();
        return app;
    }

    public static IServiceCollection AddHackSystemWebAPIExtensions(this IServiceCollection services)
    {
        services
            .AddScoped<IWebAPILogDataService, WebAPILogDataService>()
            .AddScoped<RecyclableMemoryStreamManager>();

        return services;
    }
}
