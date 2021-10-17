using HackSystem.Web.ProgramSchedule.Entity;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

public class ProgramIconMouseEventArgs : MouseEventArgs
{
    public ProgramIconMouseEventArgs(UserProgramMap userProgramMap)
    {
        this.UserProgramMap = userProgramMap;
    }

    public UserProgramMap UserProgramMap { get; init; }
}
