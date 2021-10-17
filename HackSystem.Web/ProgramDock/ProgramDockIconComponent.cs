using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockIconComponent
{
    public async Task OnClick(MouseEventArgs args)
    {
        if (!this.OnIconClick.HasDelegate)
        {
            return;
        }

        var eventArgs = new ProgramIconMouseEventArgs(this.UserProgramMap);
        this.mapper.Map(args, eventArgs);
        await this.OnIconClick.InvokeAsync(eventArgs);
    }
}
