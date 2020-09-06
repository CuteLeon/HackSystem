using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.DataAccess
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 初始化数据库
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
                logger.LogDebug($"检查数据库创建...");
                await dbContext.Database.EnsureCreatedAsync();
                logger.LogDebug($"检查数据库迁移...");
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    logger.LogInformation($"需要合并的被挂起迁移：\n\t{string.Join("、", pendingMigrations)}");
                    await dbContext.Database.MigrateAsync();
                }
                logger.LogDebug($"数据库检查完成");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"数据库检查失败：");
            }
        }
    }
}
