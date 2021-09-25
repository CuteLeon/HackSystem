using HackSystem.Intermediary.Application;
using HackSystem.WebAPI.Domain.Notifications;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.Infrastructure.NotificationHandlers;

public class CreateAccountNotificationHandler : IIntermediaryNotificationHandler<CreateAccountNotification>
{
    private readonly ILogger<CreateAccountNotificationHandler> logger;
    private readonly IBasicProgramRepository basicProgramRepository;
    private readonly IProgramUserRepository programUserRepository;
    private readonly IUserBasicProgramMapRepository userBasicProgramMapRepository;

    public CreateAccountNotificationHandler(
        ILogger<CreateAccountNotificationHandler> logger,
        IBasicProgramRepository basicProgramRepository,
        IProgramUserRepository programUserRepository,
        IUserBasicProgramMapRepository userBasicProgramMapRepository)
    {
        this.logger = logger;
        this.basicProgramRepository = basicProgramRepository;
        this.programUserRepository = programUserRepository;
        this.userBasicProgramMapRepository = userBasicProgramMapRepository;
    }

    public async Task Handle(CreateAccountNotification notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Processing create account notification: {notification.User.UserName}...");
        var mandatoryBasicPrograms = await this.basicProgramRepository.QueryMandatoryBasicPrograms();
        var programUser = await this.programUserRepository.AddAsync(new ProgramUser
        {
            Id = notification.User.Id,
        });
        programUser = await this.programUserRepository.AddAsync(programUser);
        var userProgramMaps = mandatoryBasicPrograms
            .Select(program => new UserBasicProgramMap
            {
                UserId = notification.User.Id,
                ProgramId = program.Id,
                PinToDesktop = true,
                PinToDock = true
            });
        await userBasicProgramMapRepository.AddRangeAsync(userProgramMaps);
        this.logger.LogDebug($"Create account notification processed {notification.User.UserName}.");
    }
}
