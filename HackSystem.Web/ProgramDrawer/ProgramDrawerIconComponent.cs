using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer;

public partial class ProgramDrawerIconComponent
{
    public async Task OnDbClick(MouseEventArgs args)
    {
        if (!this.OnIconDoubleClick.HasDelegate)
        {
            return;
        }

        var eventArgs = this.mapper.Map<ProgramIconMouseEventArgs>(args);
        eventArgs.UserProgramMap = this.UserProgramMap;
        await this.OnIconDoubleClick.InvokeAsync(eventArgs);
    }
}
