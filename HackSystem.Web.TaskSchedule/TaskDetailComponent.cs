namespace HackSystem.Web.TaskSchedule;

public partial class TaskDetailComponent
{
    public TaskDetailComponent()
    {
    }

    private async Task ExecuteTask()
    {
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
