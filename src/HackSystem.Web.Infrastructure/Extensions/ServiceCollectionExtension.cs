using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Application.Authentication;
using HackSystem.Web.Application.Program;
using HackSystem.Web.Application.Program.ProgramAsset;
using HackSystem.Web.Domain.Configurations;
using HackSystem.Web.Domain.Intermediary;
using HackSystem.Web.Infrastructure.Authentication;
using HackSystem.Web.Infrastructure.IntermediaryHandler;
using HackSystem.Web.Infrastructure.Program;
using HackSystem.Web.Infrastructure.Program.ProgramAsset;

namespace HackSystem.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, WebServiceOptions webServiceOptions)
    {
        services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<IProgramDetailService, ProgramDetailService>()
            .AddScoped<IProgramAssetService, ProgramAssetService>()
            .AddIntermediaryCommandHandler<LogoutCommandHandler, LogoutCommand>(ServiceLifetime.Singleton);
        return services;
    }
}
