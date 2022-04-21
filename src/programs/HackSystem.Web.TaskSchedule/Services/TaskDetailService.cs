using System.Net.Http.Json;
using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.WebServices;

namespace HackSystem.Web.TaskSchedule.Services;

public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
{
    public TaskDetailService(
        ILogger<TaskDetailService> logger,
        IHttpClientFactory httpClientFactory)
        : base(logger, httpClientFactory)
    {
    }

    public async Task<bool> ExecuteTask(TaskDetailRequest taskDetail)
    {
        var result = await this.HttpClient.PostAsJsonAsync("api/taskserver/ExecuteTask", taskDetail);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTask(TaskDetailRequest taskDetail)
    {
        var result = await this.HttpClient.PutAsJsonAsync("api/taskserver/UpdateTask", taskDetail);
        return result.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        var result = await this.HttpClient.GetFromJsonAsync<IEnumerable<TaskDetailResponse>>("api/taskserver/QueryTasks");
        return result;
    }
}
