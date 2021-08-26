using System.Collections.Generic;
using System.Linq;
using HackSystem.WebAPI.Model.Task;
using HackSystem.WebAPI.TaskServers.DataServices;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.Services;

    public class TaskLoader : ITaskLoader
    {
        private readonly ILogger<TaskLoader> logger;
        private readonly ITaskDataService taskDataService;

        public TaskLoader(
            ILogger<TaskLoader> logger,
            ITaskDataService taskDataService)
        {
            this.logger = logger;
            this.taskDataService = taskDataService;
        }

        public IEnumerable<TaskDetail> GetTaskDetails()
        {
            this.logger.LogInformation($"Get task details...");
            var taskDetails = this.taskDataService.QueryEnabledTasks().Result;
            this.logger.LogInformation($"Get {taskDetails.Count()} Task details.");
            return taskDetails;
        }
    }
