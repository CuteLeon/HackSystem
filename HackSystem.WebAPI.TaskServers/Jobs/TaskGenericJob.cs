using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HackSystem.WebAPI.TaskServers.DataServices;
using HackSystem.WebAPI.TaskServers.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.TaskServers.Jobs
{
    public class TaskGenericJob : TaskJobBase, ITaskGenericJob
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITaskParameterWrapper taskParameterWrapper;

        public TaskGenericJob(
            ILogger<TaskGenericJob> logger,
            ITaskLogDataService taskLogDataService,
            IServiceScopeFactory serviceScopeFactory)
            : base(logger, taskLogDataService)
        {
            this.serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            this.taskParameterWrapper = this.serviceProvider.GetRequiredService<ITaskParameterWrapper>();
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
            var lazyParameterDictionary = new Lazy<Dictionary<string, string>?>(
                () => this.taskParameterWrapper.WrapTaskParameters(this.TaskDetail.Parameters));
            var parameters = parameterInfos
                .OrderBy(info => info.Position)
                .Select(info => info switch
                {
                    _ when info.HasDefaultValue => info.DefaultValue,
                    _ when info.ParameterType == typeof(Dictionary<string, string>) => lazyParameterDictionary.Value,
                    _ => Activator.CreateInstance(info.ParameterType),
                })
                .ToArray();
            var result = taskMethod.Invoke(taskInstance, parameters);
        }
    }
}
