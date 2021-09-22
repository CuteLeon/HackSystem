using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Application.Repository.ProgramAssets;
using HackSystem.WebAPI.ProgramServer.Domain.Configurations;
using HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;
using HackSystem.WebAPI.ProgramServer.Infrastructure.Repository.ProgramAssets;

namespace HackSystem.WebAPI.ProgramServer.Extensions;

public static class HackSystemProgramServerExtension
{
    public static IServiceCollection AddProgramServices(
        this IServiceCollection services)
    {
        services.AddScoped<IBasicProgramDataService, BasicProgramDataService>();
        services.AddScoped<IUserBasicProgramMapDataService, UserBasicProgramMapDataService>();

        return services;
    }

    public static IServiceCollection AddProgramAssetServices(
        this IServiceCollection services,
        Action<ProgramAssetOptions> programAssetOptionsAction)
    {
        services
            .Configure(programAssetOptionsAction)
            .AddScoped<IProgramAssetService, ProgramAssetService>();

        return services;
    }
}
