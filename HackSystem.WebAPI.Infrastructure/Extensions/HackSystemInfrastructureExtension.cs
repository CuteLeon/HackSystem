using HackSystem.WebAPI.Application.Behaviors;
using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Infrastructure.Behaviors;
using HackSystem.WebAPI.Infrastructure.Repository;

namespace HackSystem.WebAPI.Infrastructure.Extensions;

public static class HackSystemInfrastructureExtension
{
    public static IServiceCollection AddWebAPIServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAccountCreatedNotificationHandler, AccountCreatedNotificationHandler>();
        services.AddScoped<IGenericOptionRepository, GenericOptionRepository>();

        return services;
    }
}
