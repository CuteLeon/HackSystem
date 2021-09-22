using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.Domain.Entity;

namespace HackSystem.WebAPI.Infrastructure.Repository;

public class WebAPILogRepository : RepositoryBase<WebAPILog>, IWebAPILogRepository
{
    public WebAPILogRepository(
        ILogger<WebAPILogRepository> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
