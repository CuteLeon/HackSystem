using HackSystem.WebAPI.MockServer.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HackSystem.WebAPI.MockServer.Infrastructure.Extensions;

public static class HackSystemMockServerExtension
{
    public static IApplicationBuilder UseMockServerInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<MockServerMiddleware>();
        return app;
    }
}
