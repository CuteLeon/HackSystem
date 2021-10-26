using System.Drawing;
using HackSystem.Web.ProgramSchedule.Entity;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

public class ProgramIconEventArgs : EventArgs
{
    public ProgramIconEventArgs(
        UserProgramMap userProgramMap,
        MouseEventArgs args)
    {
        this.UserProgramMap = userProgramMap;
        Position = new Point((int)args.ClientX, (int)args.ClientY);
    }

    public ProgramIconEventArgs(
    UserProgramMap userProgramMap,
    TouchEventArgs args)
    {
        this.UserProgramMap = userProgramMap;
        Position = args.Touches.Any() ?
            new Point((int)args.Touches.First().ClientX, (int)args.Touches.First().ClientY) : null;
    }

    public ProgramIconEventArgs(
        UserProgramMap userProgramMap,
        Point? position)
    {
        this.UserProgramMap = userProgramMap;
        Position = position;
    }

    public Point? Position { get; init; }

    public UserProgramMap UserProgramMap { get; init; }
}
