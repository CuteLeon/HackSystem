using HackSystem.DataTransferObjects.TaskServer;
using HackSystem.Web.Authentication.WebServices;
using HackSystem.Web.Component.ToastContainer;
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
            this.serviceScope.ServiceProvider.GetRequiredService<AuthenticatedHttpClient>());
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
        await this.ToastHandler.PopupToast(new ToastDetail
        {
            Title = "Load Tasks Successfully!",
            Icon = ToastIcons.Information,
            Message = $"Load {tasks.Count()} tasks successfully.",
        });
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
                await this.ToastHandler.PopupToast(new ToastDetail
                {
                    Title = "Task Trigger Successfully!",
                    Icon = ToastIcons.Information,
                    Message = $"Task {taskDetail.TaskName} triggered successfully, please wait for complete.",
                });
            }
            else
            {
                this.Logger.LogWarning($"Trigger task {taskDetail.TaskName} failed.");
                await this.ToastHandler.PopupToast(new ToastDetail
                {
                    Title = "Task Trigger Failed.",
                    Icon = ToastIcons.Warning,
                    Message = $"Task {taskDetail.TaskName} triggered failed, please try again later.",
                });
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, $"Unable to trigger task {taskDetail.TaskName}.");
            await this.ToastHandler.PopupToast(new ToastDetail
            {
                Title = "Task Trigger Failed.",
                Icon = ToastIcons.Error,
                Message = $"Unable to trigger Task {taskDetail.TaskName}: {ex.Message}.",
            });
        }
    }
}
