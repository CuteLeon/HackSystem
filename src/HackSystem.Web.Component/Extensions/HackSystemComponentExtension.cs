using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Component.Popover;
using HackSystem.Web.Component.ToastContainer;

namespace HackSystem.Web.Component.Extensions;

public static class HackSystemComponentExtension
{
    public static IServiceCollection AddHackSystemComponent(
        this IServiceCollection services)
        => services
            .AddScoped<IToastHandler, ToastHandler>()
            .AddSingleton<IPopoverHandler, PopoverHandler>()
            .AddIntermediaryEvent<ToastEvent>();
}
