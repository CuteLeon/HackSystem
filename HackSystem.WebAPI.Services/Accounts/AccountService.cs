using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Services.API.Account;
using HackSystem.WebAPI.Services.API.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> logger;
        private readonly IBasicProgramDataService basicProgramDataService;
        private readonly IUserBasicProgramMapDataService basicProgramMapDataService;

        public AccountService(
            ILogger<AccountService> logger,
            IBasicProgramDataService basicProgramDataService,
            IUserBasicProgramMapDataService basicProgramMapDataService)
        {
            this.logger = logger;
            this.basicProgramDataService = basicProgramDataService;
            this.basicProgramMapDataService = basicProgramMapDataService;
        }

        public async Task InitialUser(HackSystemUser user)
        {
            this.logger.LogDebug($"Initial new user: {user.UserName}");

            var maps = (await basicProgramDataService.QueryIntegralBasicPrograms())
                .Select(p => new UserBasicProgramMap
                {
                    UserId = user.Id,
                    ProgramId = p.Id,
                    PinToDock = true
                });
            await basicProgramMapDataService.AddRangeAsync(maps);

            this.logger.LogDebug($"Initial successfully: {user.UserName}");
        }
    }
}
