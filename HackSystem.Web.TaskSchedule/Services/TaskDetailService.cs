using System.Net.Http.Json;
using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.Infrastructure.Authentication;

namespace HackSystem.Web.TaskSchedule.Services;

public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
{
    public TaskDetailService(
        ILogger<TaskDetailService> logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        HttpClient httpClient)
        : base(logger, authenticationTokenHandler, httpClient)
    {
    }

    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<TaskDetailResponse>>("api/taskserver/QueryTasks");
        return result;
    }
}
