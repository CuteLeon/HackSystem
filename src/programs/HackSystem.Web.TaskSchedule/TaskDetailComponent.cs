namespace HackSystem.Web.TaskSchedule;

public partial class TaskDetailComponent
{
    public TaskDetailComponent()
    {
    }

    private async Task ExecuteTask()
    {
        if (executing) return;

        this.executing = true;
        this.StateHasChanged();

        if (this.OnExecuteTask.HasDelegate)
            await this.OnExecuteTask.InvokeAsync(this.TaskDetail);

        this.executing = false;
        this.StateHasChanged();
    }

    private async Task DeleteTask()
    {
    }

    private async Task EditTask()
    {
    }

    private async Task SwitchTaskEnable(bool enable)
    {
        this.Logger.LogInformation($"Switch Task {this.TaskDetail.TaskName} ({this.TaskDetail.TaskID}) Enable to {(enable ? "Enabled" : "Disabled")}...");
    }
}
