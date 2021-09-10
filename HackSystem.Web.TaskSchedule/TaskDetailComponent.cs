namespace HackSystem.Web.TaskSchedule
{
    public partial class TaskDetailComponent
    {
        public TaskDetailComponent()
        {
        }

        private async Task ExecuteTask()
        {
        }

        private async Task EditTask()
        {
        }

        private async Task SwitchTaskEnable()
        {
            this.TaskDetail.Enabled = !this.TaskDetail.Enabled;
            this.StateHasChanged();
        }
    }
}
