using FluentScheduler;
using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.Jobs
{
    public class TaskGenericJob : IJob
    {
        public TaskDetail TaskDetail { get; set; }

        public TaskGenericJob(TaskDetail taskDetail)
        {
            this.TaskDetail = taskDetail;
        }

        public void Execute()
        {
            // TODO: Task Generic Job
        }
    }
}
