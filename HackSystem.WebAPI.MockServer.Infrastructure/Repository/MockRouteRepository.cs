using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.MockServer.Application.Repository;
using HackSystem.WebAPI.MockServer.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HackSystem.WebAPI.MockServer.Infrastructure.Repository;

public class MockRouteRepository : RepositoryBase<MockRouteDetail>, IMockRouteRepository
{
    private readonly IMemoryCache memoryCache;

    public MockRouteRepository(
        ILogger<MockRouteRepository> logger,
        IMemoryCache memoryCache,
        DbContext dbContext)
        : base(logger, dbContext)
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
