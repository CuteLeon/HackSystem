using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.MockServer.Domain.Entity;

namespace HackSystem.WebAPI.MockServer.Application.Repository;

public interface IMockRouteRepository : IRepositoryBase<MockRouteDetail>
{
    Task<MockRouteDetail?> QueryMockRoute(string uri, string method, string sourceHost);
}
