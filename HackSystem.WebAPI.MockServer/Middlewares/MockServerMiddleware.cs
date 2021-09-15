using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HackSystem.WebAPI.MockServer.Services;
using HackSystem.WebAPI.MockServers.DataServices;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServer.Middlewares;

public class MockServerMiddleware
{
    private readonly ILogger<MockServerMiddleware> logger;
    private readonly IMockRouteDataService mockRouteDataService;
    private readonly IMockRouteLogDataService mockRouteLogDataService;
    private readonly IMockRouteResponseWrapper mockRouteResponseWrapper;
    private readonly IMockForwardRequestWrapper mockForwardRequestWrapper;
    private readonly IHttpClientFactory httpClientFactory;
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
        this.mockRouteResponseWrapper = serviceScope.ServiceProvider.GetRequiredService<IMockRouteResponseWrapper>();
        this.mockForwardRequestWrapper = serviceScope.ServiceProvider.GetRequiredService<IMockForwardRequestWrapper>();
        this.httpClientFactory = serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var uri = context.Request.Path.ToUriComponent();
        var method = context.Request.Method;
        var sourceHost = context.Connection.RemoteIpAddress.ToString();
        var mockRoute = await this.mockRouteDataService.QueryMockRoute(uri, method, sourceHost);
        if (mockRoute == null)
        {
            await this.next(context);
            return;
        }

        this.logger.LogInformation($"Mock Route for {sourceHost} in template {mockRoute.RouteName}({mockRoute.RouteID}): [{method}]=>{uri}");
        using var requestStreamReader = new StreamReader(context.Request.Body);
        var requestContent = await requestStreamReader.ReadToEndAsync();
        var mockRouteLog = new MockRouteLogDetail
        {
            RouteID = mockRoute.RouteID,
            URI = $"{uri}{context.Request.QueryString}",
            Method = method,
            SourceHost = $"{sourceHost}:{context.Connection.RemotePort}",
            StartDateTime = DateTime.Now,
            ConnectionID = context.TraceIdentifier,
            RequestBody = requestContent,
            MockType = mockRoute.MockType,
            MockRouteLogStatus = MockRouteLogStatus.Processing,
        };
        await this.mockRouteLogDataService.AddAsync(mockRouteLog);

        try
        {
            this.mockRouteResponseWrapper.WrapMockResponse(context, mockRoute, in requestContent, out var responseContent);

            mockRouteLog.StatusCode = context.Response.StatusCode;
            mockRouteLog.ResponseBody = responseContent;
            await mockRouteLogDataService.UpdateAsync(mockRouteLog);

            await Task.Delay(mockRoute.DelayDuration);

            if (!string.IsNullOrWhiteSpace(mockRoute.ForwardAddress))
            {
                var forwardRequest = this.mockForwardRequestWrapper.WrapForwardRequest(context, mockRoute, in requestContent, out var forwardRequestContent);
                mockRouteLog.ForwardAddress = forwardRequest.RequestUri?.AbsoluteUri;
                mockRouteLog.ForwardDateTime = DateTime.Now;
                mockRouteLog.ForwardMethod = forwardRequest.Method.ToString();
                mockRouteLog.ForwardMockType = mockRoute.ForwardMockType;
                await mockRouteLogDataService.UpdateAsync(mockRouteLog);

                var httpClient = this.httpClientFactory.CreateClient();

                var forwardResponse = await httpClient.SendAsync(forwardRequest);
                var forwardResponseContent = await forwardResponse.Content.ReadAsStringAsync();
                mockRouteLog.ForwardResponseStatusCode = forwardResponse.StatusCode;
                mockRouteLog.ForwardResponseBody = forwardResponseContent;
                await mockRouteLogDataService.UpdateAsync(mockRouteLog);
            }
        }
        catch (Exception ex)
        {
            mockRouteLog.MockRouteLogStatus = MockRouteLogStatus.Failed;
            mockRouteLog.Exception = ex.ToString();

            throw;
        }
        finally
        {
            if (mockRouteLog.MockRouteLogStatus != MockRouteLogStatus.Failed)
                mockRouteLog.MockRouteLogStatus = MockRouteLogStatus.Complete;
            mockRouteLog.FinishDateTime = DateTime.Now;
            await this.mockRouteLogDataService.UpdateAsync(mockRouteLog);
            this.logger.LogInformation($"Mocked Route from {sourceHost} in template {mockRoute.RouteName}({mockRoute.RouteID}): [{method}]=>{uri}");
        }
    }
}
