using System.Net.Http.Json;
using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.WebServices;

namespace HackSystem.Web.TaskSchedule.Services;

public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
{
    public TaskDetailService(
        ILogger<TaskDetailService> logger,
        AuthenticatedHttpClient httpClient)
        : base(logger, httpClient)
    {
    }

    public async Task<bool> ExecuteTask(TaskDetailRequest taskDetail)
    {
        var result = await this.httpClient.PostAsJsonAsync("api/taskserver/ExecuteTask", taskDetail);
        return result.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        await httpClient.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<TaskDetailResponse>>("api/taskserver/QueryTasks");
        return result;
    }
}
