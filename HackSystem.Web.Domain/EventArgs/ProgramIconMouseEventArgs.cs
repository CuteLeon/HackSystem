using HackSystem.Web.ProgramSchedule.Domain.Entity;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

public class ProgramIconMouseEventArgs : MouseEventArgs
{
    public UserProgramMap UserProgramMap{ get; set; }
}
