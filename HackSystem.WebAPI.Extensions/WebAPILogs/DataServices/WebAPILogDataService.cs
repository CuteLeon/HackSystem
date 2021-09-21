using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.Model.WebLog;

namespace HackSystem.WebAPI.Extensions.WebAPILogs.DataServices;

public class WebAPILogDataService : RepositoryBase<WebAPILog>, IWebAPILogDataService
{
    public WebAPILogDataService(
        ILogger<WebAPILogDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
