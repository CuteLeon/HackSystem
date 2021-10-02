using HackSystem.Intermediary.Extensions;
using HackSystem.Web.ProgramLayer;
using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.Extensions;

public static class HackSystemWebIntermediaryExtension
{
    public static IServiceCollection AddHackSystemWebIntermediary(this IServiceCollection services)
        => services.AddHackSystemIntermediary()
            .AddHackSystemNotificationHandler<ProgramContainerComponent, ProgramLaunchNotification>(ServiceLifetime.Singleton)
            .AddHackSystemNotificationHandler<ProgramContainerComponent, ProcessDisposeNotification>(ServiceLifetime.Singleton);
}
