using HackSystem.Cryptography.Options;
using HackSystem.Cryptography.RSACryptography;

namespace HackSystem.Cryptography;

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
