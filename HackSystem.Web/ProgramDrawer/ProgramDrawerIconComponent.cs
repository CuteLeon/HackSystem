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

        var eventArgs = new ProgramIconMouseEventArgs(this.UserProgramMap);
        this.mapper.Map(args, eventArgs);
        await this.OnIconDoubleClick.InvokeAsync(eventArgs);
    }
}
