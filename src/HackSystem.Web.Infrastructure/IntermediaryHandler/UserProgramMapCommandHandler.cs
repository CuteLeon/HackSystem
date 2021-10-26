using HackSystem.Intermediary.Application;
using HackSystem.Web.Application.Program;
using HackSystem.Web.Domain.Intermediary;

namespace HackSystem.Web.Infrastructure.IntermediaryHandler;

public class UserProgramMapCommandHandler : IIntermediaryCommandHandler<UserProgramMapCommand>
{
    private readonly ILogger<UserProgramMapCommandHandler> logger;
    private readonly IProgramDetailService programDetailService;
    private readonly IIntermediaryPublisher publisher;

    public UserProgramMapCommandHandler(
        ILogger<UserProgramMapCommandHandler> logger,
        IIntermediaryPublisher publisher,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.publisher = publisher;
        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.programDetailService = serviceProvider.GetRequiredService<IProgramDetailService>();
    }

    public async Task<ValueTuple> Handle(UserProgramMapCommand command, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle user program map command ...");
        if (await this.programDetailService.UpdateUserProgram(command.UserProgramMap))
        {
            this.logger.LogInformation($"User program map command handled.");
            await this.publisher.PublishEvent(new UserProgramMapEvent(command.UserProgramMap));
        }
        else
        {
            this.logger.LogWarning($"User program map command failed to handle.");
        }
        return ValueTuple.Create();
    }
}
