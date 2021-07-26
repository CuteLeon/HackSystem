using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.DataServices
{
    public class TaskDataService : DataServiceBase<TaskDetail>, ITaskDataService
    {
        public TaskDataService(
            ILogger<TaskDataService> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<IEnumerable<TaskDetail>> QueryEnabledTasks()
        {
            return this.AsQueryable().AsNoTracking().Where(task => task.Enabled);
        }
    }
}
