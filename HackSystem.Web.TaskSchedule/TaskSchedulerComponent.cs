﻿using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.TaskSchedule.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.TaskSchedule
{
    public partial class TaskSchedulerComponent
    {
        private ITaskDetailService taskDetailService;
        private IServiceScope serviceScope;

        public TaskSchedulerComponent()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.serviceScope = this.ServiceScopeFactory.CreateScope();
            this.taskDetailService = new TaskDetailService(
                this.serviceScope.ServiceProvider.GetRequiredService<ILogger<TaskDetailService>>(),
                this.serviceScope.ServiceProvider.GetRequiredService<AuthenticationStateProvider>() as IHackSystemAuthenticationStateProvider,
                this.serviceScope.ServiceProvider.GetRequiredService<HttpClient>());
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await this.LoadTasks();
            }
        }

        private async Task LoadTasks()
        {
            await this.ClearTasks();
            var tasks = await this.taskDetailService.QueryTasks();
            this.TaskDetails.AddRange(tasks);
            this.StateHasChanged();
        }

        private async Task ClearTasks()
        {
            this.TaskDetails.Clear();
            this.StateHasChanged();
        }

        private async Task AddTasks()
        {
        }
    }
}