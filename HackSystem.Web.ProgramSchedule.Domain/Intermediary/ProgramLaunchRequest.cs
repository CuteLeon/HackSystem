using HackSystem.Intermediary.Domain;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

namespace HackSystem.Web.ProgramSchedule.Domain.Intermediary;

public class ProgramLaunchRequest : IIntermediaryRequest<ProgramLaunchResponse>
{
    public ProgramDetail ProgramDetail { get; set; }
}
