using System.Reflection;
using HackSystem.WebAPI.TaskServer.Application.Repository;
using HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;

namespace HackSystem.WebAPI.TaskServer.Jobs;

public class TaskGenericJob : TaskJobBase, ITaskGenericJob
{
    private readonly IServiceProvider serviceProvider;
    private readonly ITaskPairParameterWrapper taskPairParameterWrapper;
    private readonly ITaskJsonParameterWrapper taskJsonParameterWrapper;

    public TaskGenericJob(
        ILogger<TaskGenericJob> logger,
        ITaskLogRepository taskLogRepository,
        IServiceScopeFactory serviceScopeFactory)
        : base(logger, taskLogRepository)
    {
        this.serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.taskPairParameterWrapper = this.serviceProvider.GetRequiredService<ITaskPairParameterWrapper>();
        this.taskJsonParameterWrapper = this.serviceProvider.GetRequiredService<ITaskJsonParameterWrapper>();
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
        var lazyPairParameterDictionary = new Lazy<IDictionary<string, string>?>(
            () => this.taskPairParameterWrapper.WrapTaskParameters(this.TaskDetail.Parameters));
        var parameters = parameterInfos
            .OrderBy(info => info.Position)
            .Select(info => info switch
            {
                _ when info.HasDefaultValue => info.DefaultValue,
                _ when typeof(IDictionary<string, string>).IsAssignableFrom(info.ParameterType) => lazyPairParameterDictionary.Value,
                _ => this.taskJsonParameterWrapper.WrapTaskParameters(this.TaskDetail.Parameters, info.ParameterType),
            })
            .ToArray();

        var result = taskMethod.Invoke(taskInstance, parameters);
        if (result is Task asyncTask)
        {
            asyncTask.ConfigureAwait(false).GetAwaiter().GetResult();
            if (asyncTask.Exception != null)
                throw asyncTask.Exception;
        }
    }
}
