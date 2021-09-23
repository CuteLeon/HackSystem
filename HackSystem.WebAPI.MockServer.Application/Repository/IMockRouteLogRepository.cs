using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.MockServer.Domain.Entity;

namespace HackSystem.WebAPI.MockServer.Application.Repository;

public interface IMockRouteLogRepository : IRepositoryBase<MockRouteLogDetail>
{
}
