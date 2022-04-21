using HackSystem.DataTransferObjects.TaskServer;

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
        if (this.OnUpdateTask.HasDelegate)
            await this.OnUpdateTask.InvokeAsync(new TaskDetailRequest() { TaskID = this.TaskDetail.TaskID, Enabled = enable });
    }
}
