using System;
using System.Security.Cryptography;
using HackSystem.Cryptography.Options;
using HackSystem.Cryptography.RSACryptography;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Cryptography
{
    public static class RSACryptographyExtension
    {
        public static IServiceCollection AddRSACryptography(this IServiceCollection services, Action<RSACryptographyOptions> configure)
        {
            services
                .Configure(configure)
                .AddScoped<RSACryptoServiceProvider, RSACryptoServiceProvider>()
                .AddScoped<IRSACryptographyService, RSACryptographyService>();

            return services;
        }
    }
}
