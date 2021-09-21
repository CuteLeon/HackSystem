using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebAPI.Services.Programs;
using HackSystem.WebAPI.Services.Programs.ProgramAsset;

namespace HackSystem.WebAPI.Services.Extensions;

public static class ServiceCollectionExtension
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
