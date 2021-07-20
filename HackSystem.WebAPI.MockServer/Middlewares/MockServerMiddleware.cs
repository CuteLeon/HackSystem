using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.MockServers.DataServices;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServer.Middlewares
{
    public class MockServerMiddleware
    {
        private readonly ILogger<MockServerMiddleware> logger;
        private readonly IMockRouteDataService mockRouteDataService;
        private readonly IMockRouteLogDataService mockRouteLogDataService;
        private readonly RequestDelegate next;

        public MockServerMiddleware(
            ILogger<MockServerMiddleware> logger,
            IServiceScopeFactory serviceScopeFactory,
            RequestDelegate next)
        {
            this.logger = logger;
            var serviceScope = serviceScopeFactory.CreateScope();
            this.mockRouteDataService = serviceScope.ServiceProvider.GetRequiredService<IMockRouteDataService>();
            this.mockRouteLogDataService = serviceScope.ServiceProvider.GetRequiredService<IMockRouteLogDataService>();
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var uri = context.Request.Path.ToUriComponent();
            await this.next(context);
        }
    }
}
