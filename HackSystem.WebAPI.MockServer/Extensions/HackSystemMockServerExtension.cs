using HackSystem.WebAPI.MockServer.Application.Repository;
using HackSystem.WebAPI.MockServer.Application.Wrappers;
using HackSystem.WebAPI.MockServer.Domain.Configurations;
using HackSystem.WebAPI.MockServer.Infrastructure.Middlewares;
using HackSystem.WebAPI.MockServer.Infrastructure.Repository;
using HackSystem.WebAPI.MockServer.Infrastructure.Wrappers;
using Microsoft.AspNetCore.Builder;

namespace HackSystem.WebAPI.MockServer.Extensions;

public static class HackSystemMockServerExtension
{
    public static IServiceCollection AttachMockServer(
        this IServiceCollection services,
        MockServerOptions configuration)
    {
        services
            .Configure(new Action<MockServerOptions>(options => options.MockServerHost = configuration.MockServerHost))
            .AddScoped<IMockRouteRepository, MockRouteRepository>()
            .AddScoped<IMockRouteLogRepository, MockRouteLogRepository>()
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
