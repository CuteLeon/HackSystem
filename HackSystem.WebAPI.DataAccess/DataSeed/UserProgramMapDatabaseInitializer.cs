using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using HackSystem.WebAPI.Model.Map.UserMap;

namespace HackSystem.WebAPI.DataAccess.SeedData
{
    public static class UserProgramMapDatabaseInitializer
    {
        public static IHost InitializeUserProgramMapData(this IHost host)
        {
            InitializeUserProgramMapDataAsync(host).ConfigureAwait(false);

            return host;
        }

        public static async Task InitializeUserProgramMapDataAsync(IHost host)
        {
            var serviceScope = host.Services.CreateScope();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<HackSystemDBContext>>();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<HackSystemDBContext>();

            foreach (var user in dbContext.Users)
            {
                foreach (var program in dbContext.BasicPrograms)
                {
                    if (await dbContext.UserProgramMaps.FindAsync(user.Id, program.Id) == null)
                    {
                        logger.LogInformation($"为用户 {user.Id} 和程序 {program.Id} 添加关系 Map...");
                        await dbContext.UserProgramMaps.AddAsync(new UserProgramMap()
                        {
                            UserId = user.Id,
                            ProgramId = program.Id,
                            Hide = false,
                        });
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
