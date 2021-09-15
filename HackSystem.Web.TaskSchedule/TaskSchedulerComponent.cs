using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.TaskSchedule.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.TaskSchedule;

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
            this.serviceScope.ServiceProvider.GetRequiredService<IHackSystemAuthenticationStateProvider>(),
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
        this.Logger.LogInformation($"Loading tasks...");
        await this.ClearTasks();
        var tasks = await this.taskDetailService.QueryTasks();
        this.TaskDetails.AddRange(tasks);
        this.Logger.LogInformation($"Loaded {tasks.Count()} tasks.");
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
