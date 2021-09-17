using HackSystem.WebAPI.Services.Accounts;
using HackSystem.WebAPI.Services.API.Account;
using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebAPI.Services.Options;
using HackSystem.WebAPI.Services.Programs;
using HackSystem.WebAPI.Services.Programs.ProgramAsset;

namespace HackSystem.WebAPI.Services.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebAPIServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IBasicProgramDataService, BasicProgramDataService>();
        services.AddScoped<IUserBasicProgramMapDataService, UserBasicProgramMapDataService>();
        services.AddScoped<IGenericOptionDataService, GenericOptionDataService>();

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
