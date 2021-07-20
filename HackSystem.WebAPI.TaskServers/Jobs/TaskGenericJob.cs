using System;
using System.Linq;
using System.Reflection;
using HackSystem.WebAPI.TaskServers.DataServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.Jobs
{
    public class TaskGenericJob : TaskJobBase, ITaskGenericJob
    {
        private readonly IServiceProvider serviceProvider;

        public TaskGenericJob(
            ILogger<TaskGenericJob> logger,
            ITaskLogDataService taskLogDataService,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, taskLogDataService)
        {
            this.serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        }

        protected override void ExecuteTask()
        {
            var assemblyName = this.TaskDetail.AssemblyName;
            var typeName = this.TaskDetail.ClassName;
            var methodName = this.TaskDetail.ProcedureName;

            var taskAssembly = Assembly.Load(new AssemblyName(assemblyName));
            var taskType = taskAssembly == null ? Type.GetType(typeName) : taskAssembly.GetType(typeName);
            if (taskType == null)
            {
                throw new TypeLoadException($"Can not find task type {typeName} in target or self assembly {assemblyName}.");
            }
            var taskMethod = taskType.GetMethod(methodName);
            if (taskMethod == null)
            {
                throw new TypeLoadException($"Can not find task method {methodName} in target type {typeName}.");
            }

            var taskInstance = this.serviceProvider.GetRequiredService(taskType);
            var parameterInfos = taskMethod.GetParameters();
            var parameters = parameterInfos
                .OrderBy(info => info.Position)
                .Select(info => info.HasDefaultValue ? info.DefaultValue : Activator.CreateInstance(info.ParameterType))
                .ToArray();
            var result = taskMethod.Invoke(taskInstance, parameters);
        }
    }
}
