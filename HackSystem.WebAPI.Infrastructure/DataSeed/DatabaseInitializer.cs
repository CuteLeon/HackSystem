using HackSystem.WebAPI.Infrastructure.DBContexts;
using Microsoft.Extensions.Hosting;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class DatabaseInitializer
{
    /// <summary>
    /// Initinalize database
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static IHost InitializeDatabase(this IHost host)
    {
        if (host == null)
        {
            throw new ArgumentNullException(nameof(host));
        }

        InitializeDatabaseAsync(host).ConfigureAwait(false);

        return host;
    }

    private async static Task InitializeDatabaseAsync(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<IHost>>();
        var dbContext = services.GetRequiredService<HackSystemDbContext>();

        try
        {
            logger.LogDebug($"Ensure database created...");
            await dbContext.Database.EnsureCreatedAsync();
            logger.LogDebug($"Check database pending migrations...");
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation($"Pending database migration should be combined: \n\t{string.Join(",", pendingMigrations)}");
                await dbContext.Database.MigrateAsync();
            }
            logger.LogDebug($"Database check finished.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Database check failed.");
        }
    }
}
