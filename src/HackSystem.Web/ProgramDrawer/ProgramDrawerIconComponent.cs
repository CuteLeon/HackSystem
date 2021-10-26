using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer;

public partial class ProgramDrawerIconComponent
{
    protected async Task OnDbClick(MouseEventArgs args)
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap, args);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }

    protected async Task OnTouchEnd(TouchEventArgs args)
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap, args);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }

    public async Task OnContextMenu(MouseEventArgs args)
    {
        if (!this.OnIconContextMenu.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap, args);
        await this.OnIconContextMenu.InvokeAsync(eventArgs);
    }
}
