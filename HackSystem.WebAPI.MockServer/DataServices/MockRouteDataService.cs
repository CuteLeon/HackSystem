using System;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServers.DataServices;

public class MockRouteDataService : DataServiceBase<MockRouteDetail>, IMockRouteDataService
{
    private readonly IMemoryCache memoryCache;

    public MockRouteDataService(
        ILogger<MockRouteDataService> logger,
        IMemoryCache memoryCache,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
        this.memoryCache = memoryCache;
    }

    public async Task<MockRouteDetail?> QueryMockRoute(string uri, string method, string sourceHost)
    {
        if (string.IsNullOrWhiteSpace(uri))
        {
            throw new ArgumentNullException(nameof(uri), $"'{nameof(uri)}' cannot be null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(method))
        {
            throw new ArgumentNullException(nameof(method), $"'{nameof(method)}' cannot be null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(sourceHost))
        {
            throw new ArgumentNullException(nameof(sourceHost), $"'{nameof(sourceHost)}' cannot be null or whitespace.");
        }

        uri = uri.StartsWith("/") ? uri : $"/{uri}";
        uri = uri.EndsWith("/") ? uri.Remove(uri.Length - 1) : uri;

        var mockRoutes = await this.memoryCache.GetOrCreateAsync(
            nameof(MockRouteDetail),
            cache => Task.Factory.StartNew(() => this.AsQueryable().Where(m => m.Enabled).ToDictionary(m => m.RouteID, m => m)));

        var mockRoute = mockRoutes.Values.AsQueryable()
            .Where(m =>
                m.MockURI == uri &&
                (m.MockMethod == null ||
                    m.MockMethod == string.Empty ||
                    m.MockMethod == method) &&
                (m.MockSourceHost == null ||
                    m.MockSourceHost == string.Empty ||
                    sourceHost.StartsWith(m.MockSourceHost) ||
                    m.MockSourceHost == sourceHost) &&
                m.Enabled)
            .OrderByDescending(m => m.MockMethod)
            .ThenByDescending(m => m.MockSourceHost)
            .FirstOrDefault();
        return mockRoute;
    }
}
