using HackSystem.WebAPI.Application.Behaviors;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.Infrastructure.Behaviors;

public class AccountCreatedNotificationHandler : IAccountCreatedNotificationHandler
{
    private readonly ILogger<AccountCreatedNotificationHandler> logger;
    private readonly IBasicProgramRepository basicProgramRepository;
    private readonly IUserBasicProgramMapRepository userBasicProgramMapRepository;

    public AccountCreatedNotificationHandler(
        ILogger<AccountCreatedNotificationHandler> logger,
        IBasicProgramRepository basicProgramRepository,
        IUserBasicProgramMapRepository userBasicProgramMapRepository)
    {
        this.logger = logger;
        this.basicProgramRepository = basicProgramRepository;
        this.userBasicProgramMapRepository = userBasicProgramMapRepository;
    }

    public async Task InitialUser(HackSystemUser user)
    {
        this.logger.LogDebug($"Initial new user: {user.UserName}");

        var maps = (await basicProgramRepository.QueryIntegralBasicPrograms())
            .Select(p => new UserBasicProgramMap
            {
                UserId = user.Id,
                ProgramId = p.Id,
                PinToDock = true
            });
        await userBasicProgramMapRepository.AddRangeAsync(maps);

        this.logger.LogDebug($"Initial successfully: {user.UserName}");
    }
}
