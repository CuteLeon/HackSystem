using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.MockServer.Domain.Entity;

namespace HackSystem.WebAPI.MockServer.Application.Repository;

public interface IMockRouteRepository : IRepositoryBase<MockRouteDetail>
{
    Task<IEnumerable<MockRouteDetail>> QueryMockRoutes();

    Task<MockRouteDetail?> QueryMockRoute(string uri, string method, string sourceHost);
}
