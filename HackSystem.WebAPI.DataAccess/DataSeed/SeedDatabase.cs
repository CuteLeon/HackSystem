using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.DataAccess.SeedData
{
    public static class SeedDatabase
    {
        /// <summary>
        /// 填充种子数据
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost SeedData(this IHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            InitializeDatabase(host).ConfigureAwait(false);

            return host;
        }

        private async static Task InitializeDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<IHost>>();
            var dbContext = services.GetRequiredService<HackSystemDBContext>();

            try
            {
                logger.LogDebug($"开始数据库自动迁移...");
                dbContext.Database.Migrate();
                logger.LogDebug($"数据库自动迁移完成");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"数据库自动迁移失败：");
            }

            try
            {
                await SeedIdentityData.InitializeAsync(services);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "填充种子数据遇到异常");
            }
        }
    }
}
