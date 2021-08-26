using HackSystem.WebAPI.MockServer.Middlewares;
using HackSystem.WebAPI.MockServer.Services;
using HackSystem.WebAPI.MockServers.Configurations;
using HackSystem.WebAPI.MockServers.DataServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServers.Extensions;

public static class HackSystemMockServerExtension
{
    public static IServiceCollection AttachMockServer(
        this IServiceCollection services,
        MockServerOptions configuration)
    {
        services
            .Configure(new Action<MockServerOptions>(options => options.MockServerHost = configuration.MockServerHost))
            .AddScoped<IMockRouteDataService, MockRouteDataService>()
            .AddScoped<IMockRouteLogDataService, MockRouteLogDataService>()
            .AddScoped<IMockRouteResponseWrapper, MockRouteResponseWrapper>()
            .AddScoped<IMockForwardRequestWrapper, MockForwardRequestWrapper>();

        return services;
    }

    public static IApplicationBuilder UseMockServer(this IApplicationBuilder app)
    {
        app.UseMiddleware<MockServerMiddleware>();
        return app;
    }
}
