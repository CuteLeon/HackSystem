using HackSystem.WebAPI.Services.API.DataServices.Program;
using HackSystem.WebAPI.Services.DataServices.Program;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.WebAPI.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IBasicProgramDataService, BasicProgramDataService>();

            return services;
        }
    }
}
