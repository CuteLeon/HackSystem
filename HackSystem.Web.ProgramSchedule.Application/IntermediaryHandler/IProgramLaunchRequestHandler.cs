using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Application.IntermediaryHandler;

public interface IProgramLaunchRequestHandler : IIntermediaryRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>
{
}
