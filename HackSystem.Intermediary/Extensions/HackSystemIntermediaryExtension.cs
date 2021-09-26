using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Infrastructure;

namespace HackSystem.Intermediary.Extensions;

public static class HackSystemIntermediaryExtension
{
    public static IServiceCollection AddHackSystemIntermediary(this IServiceCollection services)
        => services
            .AddMediatR(typeof(HackSystemIntermediaryExtension).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(IntermediaryPipelineBehavior<,>))
            .AddTransient<IIntermediaryNotificationPublisher, IntermediaryNotificationPublisher>()
            .AddTransient<IIntermediaryCommandSender, IntermediaryCommandSender>()
            .AddTransient<IIntermediaryRequestSender, IntermediaryRequestSender>();
}
