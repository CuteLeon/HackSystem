using HackSystem.WebAPI.TaskServer.Infrastructure.Wrapper;

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
