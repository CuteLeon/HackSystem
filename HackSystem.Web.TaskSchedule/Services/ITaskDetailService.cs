using HackSystem.WebDataTransfer.TaskServer;

namespace HackSystem.Web.TaskSchedule.Services
{
    public interface ITaskDetailService
    {
        Task<IEnumerable<TaskDetailDTO>> QueryTasks();
    }
}
