using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.MockServer.Application.Repository;
using HackSystem.WebAPI.MockServer.Domain.Entity;

namespace HackSystem.WebAPI.MockServer.Infrastructure.Repository;

public class MockRouteLogRepository : RepositoryBase<MockRouteLogDetail>, IMockRouteLogRepository
{
    public MockRouteLogRepository(
        ILogger<MockRouteLogRepository> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
