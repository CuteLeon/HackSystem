using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.IntermediaryHandler;

public interface IProgramLaunchRequestHandler : IIntermediaryRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>
{
}
