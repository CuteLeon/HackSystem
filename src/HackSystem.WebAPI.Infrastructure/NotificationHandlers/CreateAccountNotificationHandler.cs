using HackSystem.Intermediary.Application;
using HackSystem.WebAPI.Domain.Notifications;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.Infrastructure.NotificationHandlers;

public class CreateAccountNotificationHandler : IIntermediaryNotificationHandler<CreateAccountNotification>
{
    private readonly ILogger<CreateAccountNotificationHandler> logger;
    private readonly IProgramDetailRepository programDetailRepository;
    private readonly IProgramUserRepository programUserRepository;
    private readonly IUserProgramMapRepository userProgramMapRepository;

    public CreateAccountNotificationHandler(
        ILogger<CreateAccountNotificationHandler> logger,
        IProgramDetailRepository programDetailRepository,
        IProgramUserRepository programUserRepository,
        IUserProgramMapRepository userProgramMapRepository)
    {
        this.logger = logger;
        this.programDetailRepository = programDetailRepository;
        this.programUserRepository = programUserRepository;
        this.userProgramMapRepository = userProgramMapRepository;
    }

    public async Task Handle(CreateAccountNotification notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Processing create account notification: {notification.User.UserName}...");
        var mandatoryPrograms = await this.programDetailRepository.QueryMandatoryPrograms();
        var programUser = await this.programUserRepository.AddAsync(new ProgramUser
        {
            Id = notification.User.Id,
        });
        var userProgramMaps = mandatoryPrograms
            .Select(program => new UserProgramMap
            {
                UserId = notification.User.Id,
                ProgramId = program.Id,
                PinToDesktop = true,
                PinToDock = true
            });
        await userProgramMapRepository.AddRangeAsync(userProgramMaps);
        this.logger.LogDebug($"Create account notification processed {notification.User.UserName}.");
    }
}
