using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockIconComponent
{
    public async Task OnClick(MouseEventArgs args)
        => await this.RaiseIconSelect();

    public async Task OnTouchEnd(TouchEventArgs args)
        => await this.RaiseIconSelect();

    protected async Task RaiseIconSelect()
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }
}
