using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.MockServers.DataServices;

public class MockRouteLogDataService : DataServiceBase<MockRouteLogDetail>, IMockRouteLogDataService
{
    public MockRouteLogDataService(
        ILogger<MockRouteLogDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
