using System.Net.Http.Json;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.Authentication;
using HackSystem.DataTransferObjects.TaskServer;

namespace HackSystem.Web.TaskSchedule.Services;

public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
{
    public TaskDetailService(
        ILogger<TaskDetailService> logger,
        IHackSystemAuthenticationStateProvider hackSystemAuthenticationStateProvider,
        HttpClient httpClient)
        : base(logger, hackSystemAuthenticationStateProvider, httpClient)
    {
    }

    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<TaskDetailResponse>>("api/taskserver/QueryTasks");
        return result;
    }
}
