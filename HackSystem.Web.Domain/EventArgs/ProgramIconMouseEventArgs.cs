using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

public class ProgramIconEventArgs : EventArgs
{
    public ProgramIconEventArgs(UserProgramMap userProgramMap)
    {
        this.UserProgramMap = userProgramMap;
    }

    public UserProgramMap UserProgramMap { get; init; }
}
