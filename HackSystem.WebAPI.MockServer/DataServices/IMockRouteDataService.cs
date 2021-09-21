using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.Model.Mock;

namespace HackSystem.WebAPI.MockServers.DataServices;

public interface IMockRouteDataService : IRepositoryBase<MockRouteDetail>
{
    Task<MockRouteDetail?> QueryMockRoute(string uri, string method, string sourceHost);
}
