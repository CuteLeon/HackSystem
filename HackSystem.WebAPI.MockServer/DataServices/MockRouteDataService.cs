using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServers.DataServices
{
    public class MockRouteDataService : DataServiceBase<MockRouteDetail>, IMockRouteDataService
    {
        public MockRouteDataService(
            ILogger<MockRouteDataService> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<MockRouteDetail> QueryMockRoute(string uri, string method, string sourceHost)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException($"'{nameof(uri)}' cannot be null or whitespace.", nameof(uri));
            }

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException($"'{nameof(method)}' cannot be null or whitespace.", nameof(method));
            }

            if (string.IsNullOrWhiteSpace(sourceHost))
            {
                throw new ArgumentNullException($"'{nameof(sourceHost)}' cannot be null or whitespace.", nameof(sourceHost));
            }

            uri = uri.StartsWith("/") ? uri : $"/{uri}";
            uri = uri.EndsWith("/") ? uri.Remove(uri.Length - 1) : uri;
            var mockRoute = await this.AsQueryable()
                .Where(m => m.MockURI == uri && m.MockMethod == method && m.MockSourceHost == sourceHost && m.Enabled)
                .FirstOrDefaultAsync();
            if (mockRoute != null)
                return mockRoute;

            var query = this.AsQueryable().Where(m => m.MockURI == uri && m.MockMethod == method && m.Enabled);
            var mockRoutes = await query.ToArrayAsync();
            if (mockRoutes.Any())
            {
                mockRoute = FindBestMatchMockRoute(mockRoutes, sourceHost);
                if (mockRoute != null)
                    return mockRoute;
            }

            query = this.AsQueryable().Where(m => m.MockURI == uri && (m.MockMethod == null || m.MockMethod == string.Empty) && m.Enabled);
            mockRoutes = await query.ToArrayAsync();
            if (mockRoutes.Any())
            {
                mockRoute = FindBestMatchMockRoute(mockRoutes, sourceHost);
                if (mockRoute != null)
                    return mockRoute;
            }

            return default;
        }

        private MockRouteDetail FindBestMatchMockRoute(IEnumerable<MockRouteDetail> mockRoutePool, string sourceHost)
        {
            MockRouteDetail theEqual = default, theStartWith = default, theEmpty = default;
            foreach (var mockRoute in mockRoutePool)
            {
                if (string.IsNullOrWhiteSpace(mockRoute.MockSourceHost))
                {
                    theEmpty = mockRoute;
                }
                else if (mockRoute.MockSourceHost.Equals(sourceHost, StringComparison.OrdinalIgnoreCase))
                {
                    theEqual = mockRoute;
                }
                else if (sourceHost.StartsWith(mockRoute.MockSourceHost))
                {
                    theStartWith = mockRoute;
                }
            }

            return theEqual ?? theStartWith ?? theEmpty;
        }
    }
}
