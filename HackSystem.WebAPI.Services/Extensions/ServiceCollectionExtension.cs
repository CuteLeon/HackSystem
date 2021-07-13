using HackSystem.WebAPI.Services.Accounts;
using HackSystem.WebAPI.Services.API.Account;
using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebAPI.Services.Program;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.WebAPI.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBasicProgramDataService, BasicProgramDataService>();
            services.AddScoped<IUserBasicProgramMapDataService, UserBasicProgramMapDataService>();

            return services;
        }
    }
}
