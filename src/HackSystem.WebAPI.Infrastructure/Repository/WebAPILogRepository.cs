using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.Domain.Entity;

namespace HackSystem.WebAPI.Infrastructure.Repository;

public class WebAPILogRepository : RepositoryBase<WebAPILog>, IWebAPILogRepository
{
    public WebAPILogRepository(
        ILogger<WebAPILogRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }
}
