using HackSystem.WebAPI.Application.Behaviors;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Services.API.Program;

namespace HackSystem.WebAPI.Infrastructure.Behaviors;

// TODO: [Leon] Modify to monitor notification and process by MediatR
public class AccountCreatedNotificationHandler : IAccountCreatedNotificationHandler
{
    private readonly ILogger<AccountCreatedNotificationHandler> logger;
    private readonly IBasicProgramDataService basicProgramDataService;
    private readonly IUserBasicProgramMapDataService basicProgramMapDataService;

    public AccountCreatedNotificationHandler(
        ILogger<AccountCreatedNotificationHandler> logger,
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
