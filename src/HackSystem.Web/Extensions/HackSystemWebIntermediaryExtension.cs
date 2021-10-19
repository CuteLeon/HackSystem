using HackSystem.Intermediary.Extensions;
using HackSystem.Web.ProgramLayer;

namespace HackSystem.Web.Extensions;

public static class HackSystemWebIntermediaryExtension
{
    public static IServiceCollection AddHackSystemWebIntermediary(this IServiceCollection services)
        => services.AddHackSystemIntermediary();
}
