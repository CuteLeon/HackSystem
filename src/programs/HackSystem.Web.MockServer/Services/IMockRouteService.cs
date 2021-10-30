using HackSystem.DataTransferObjects.MockServer;

namespace HackSystem.Web.MockServer.Services;

public interface IMockRouteService
{
    Task<IEnumerable<MockRouteResponse>> QueryMockRoutes();
}
