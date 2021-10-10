using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramSchedule.Intermediary;

public class ProgramLaunchRequest : IIntermediaryRequest<ProgramLaunchResponse>
{
    public ProgramDetail ProgramDetail { get; set; }
}
