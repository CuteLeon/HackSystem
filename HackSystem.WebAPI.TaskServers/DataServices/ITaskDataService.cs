using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Task;

namespace HackSystem.WebAPI.TaskServers.DataServices;

public interface ITaskDataService : IDataServiceBase<TaskDetail>
{
    Task<IEnumerable<TaskDetail>> QueryTasks();

    Task<IEnumerable<TaskDetail>> QueryEnabledTasks();
}
