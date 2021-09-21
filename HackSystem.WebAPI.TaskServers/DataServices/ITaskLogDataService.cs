using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.TaskServers.Domain.Entity;

namespace HackSystem.WebAPI.TaskServers.DataServices;

public interface ITaskLogDataService : IDataServiceBase<TaskLogDetail>
{
}
