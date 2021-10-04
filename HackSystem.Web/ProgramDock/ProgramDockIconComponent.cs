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

        var eventArgs = this.mapper.Map<ProgramIconMouseEventArgs>(args);
        eventArgs.UserProgramMap = this.UserProgramMap;
        await this.OnIconClick.InvokeAsync(eventArgs);
    }
}
