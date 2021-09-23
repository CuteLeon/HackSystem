using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.MockServer.Application.Repository;
using HackSystem.WebAPI.MockServer.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.MockServer.Infrastructure.Repository;

public class MockRouteLogRepository : RepositoryBase<MockRouteLogDetail>, IMockRouteLogRepository
{
    public MockRouteLogRepository(
        ILogger<MockRouteLogRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }
}
