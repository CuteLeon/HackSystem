using HackSystem.DataTransferObjects.TaskServer;

namespace HackSystem.Web.TaskSchedule.Services;

public interface ITaskDetailService
{
    Task<IEnumerable<TaskDetailResponse>> QueryTasks();

    Task<bool> ExecuteTask(TaskDetailRequest taskDetail);
}
