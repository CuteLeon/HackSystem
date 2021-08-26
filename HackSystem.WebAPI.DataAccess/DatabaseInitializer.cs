using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.DataAccess;

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
        var dbContext = services.GetRequiredService<HackSystemDBContext>();

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
