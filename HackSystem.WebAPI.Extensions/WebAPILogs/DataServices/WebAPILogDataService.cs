using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.WebLog;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Extensions.WebAPILogs.DataServices;

public class WebAPILogDataService : DataServiceBase<WebAPILog>, IWebAPILogDataService
{
    public WebAPILogDataService(
        ILogger<WebAPILogDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }
}
