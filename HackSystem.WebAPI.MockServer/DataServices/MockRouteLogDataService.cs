using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.Model.Mock;

namespace HackSystem.WebAPI.MockServers.DataServices;

public class MockRouteLogDataService : RepositoryBase<MockRouteLogDetail>, IMockRouteLogDataService
{
    public MockRouteLogDataService(
        ILogger<MockRouteLogDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
