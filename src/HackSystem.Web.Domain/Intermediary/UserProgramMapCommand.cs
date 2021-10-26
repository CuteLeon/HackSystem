using HackSystem.DataTransferObjects.Programs;
using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.Domain.Intermediary;

public class UserProgramMapCommand : IIntermediaryCommand
{
    public UserProgramMapCommand(
        UserProgramMapRequest userProgramMap)
    {
        UserProgramMap = userProgramMap;
    }

    public UserProgramMapRequest UserProgramMap { get; set; }
}
