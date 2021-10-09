﻿using System.Net.Http.Json;
using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.Authentication.WebService;

namespace HackSystem.Web.TaskSchedule.Services;

public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
{
    public TaskDetailService(
        ILogger<TaskDetailService> logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        IOptionsSnapshot<HackSystemAuthenticationOptions> optionsSnapshot,
        HttpClient httpClient)
        : base(logger, authenticationTokenHandler, optionsSnapshot, httpClient)
    {
    }

    public async Task<bool> ExecuteTask(TaskDetailRequest taskDetail)
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.PostAsJsonAsync("api/taskserver/ExecuteTask", taskDetail);
        return result.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<TaskDetailResponse>> QueryTasks()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<TaskDetailResponse>>("api/taskserver/QueryTasks");
        return result;
    }
}
