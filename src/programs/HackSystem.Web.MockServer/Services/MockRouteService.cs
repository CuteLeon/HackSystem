using System.Net.Http.Json;
using HackSystem.DataTransferObjects.MockServer;
using HackSystem.Web.Authentication.WebServices;

namespace HackSystem.Web.MockServer.Services;

public class MockRouteService : AuthenticatedServiceBase, IMockRouteService
{
    public MockRouteService(
        ILogger<MockRouteService> logger,
        IHttpClientFactory httpClientFactory)
        : base(logger, httpClientFactory)
    {
    }

    public async Task<IEnumerable<MockRouteResponse>> QueryMockRoutes()
    {
        var result = await this.HttpClient.GetFromJsonAsync<IEnumerable<MockRouteResponse>>("api/mockserver/QueryMockRoutes");
        return result;
    }
}
