using HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Extensions;

public static class HackSystemTaskServerInfrastructureExtension
{
    public static IServiceCollection AttachTaskServerInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddTransient<ITaskPairParameterWrapper, TaskPairParameterWrapper>()
            .AddTransient<ITaskJsonParameterWrapper, TaskJsonParameterWrapper>();

        return services;
    }
}
