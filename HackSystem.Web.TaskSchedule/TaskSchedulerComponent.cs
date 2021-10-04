using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.ProgramPlatform.Abstractions.Enums;
using HackSystem.Web.TaskSchedule.Services;

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
            this.serviceScope.ServiceProvider.GetRequiredService<IHackSystemAuthenticationTokenHandler>(),
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
        this.DesktopToastContainer?.PopToast("Load Tasks Successfully!", $"Load {tasks.Count()} tasks successfully.", ToastIcons.Information);
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

    private async Task ExecuteTask(TaskDetailResponse taskDetail)
    {
        try
        {
            this.Logger.LogInformation($"Trigger task {taskDetail.TaskName}...");
            var result = await this.taskDetailService.ExecuteTask(new TaskDetailRequest() { TaskID = taskDetail.TaskID });
            if (result)
            {
                this.Logger.LogInformation($"Trigger task {taskDetail.TaskName} successfully.");
                this.DesktopToastContainer?.PopToast("Task Trigger Successfully!", $"Task {taskDetail.TaskName} triggered successfully, please wait for complete.", ToastIcons.Information);
            }
            else
            {
                this.Logger.LogWarning($"Trigger task {taskDetail.TaskName} failed.");
                this.DesktopToastContainer?.PopToast("Task Trigger Failed.", $"Task {taskDetail.TaskName} triggered failed, please try again later.", ToastIcons.Warning);
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Unable to trigger task {taskDetail.TaskName}.");
            this.DesktopToastContainer?.PopToast("Task Trigger Failed!", $"Unable to trigger Task {taskDetail.TaskName}: {ex.Message}", ToastIcons.Error);
        }
    }
}
