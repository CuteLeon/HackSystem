using HackSystem.DataTransferObjects.Programs;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.Domain.Intermediary;

public class UserProgramMapEvent : IIntermediaryEvent
{
    public UserProgramMapEvent(
        UserProgramMapRequest userProgramMap)
    {
        UserProgramMap = userProgramMap;
    }

    public UserProgramMapRequest UserProgramMap { get; set; }
}
