using HackSystem.Web.Component.Popover;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockIconComponent
{
    public async Task OnClick(MouseEventArgs args)
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap, args);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }

    public async Task OnTouchEnd(TouchEventArgs args)
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap, args);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }

    public async Task UpdateWindowDetail(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        this.pendingRefreshWindows = true;
        this.StateHasChanged();
        this.pendingRefreshWindows = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await this.popoverHandler.SetupPopover(new PopoverDetail()
            {
                ShowDelay = 150,
                IsHtmlContent = true,
                ContentSourceId = this.DockIconPopoverContentId,
                HeaderSourceId = this.DockIconPopoverHeaderId,
                Trigger = PopoverTriggers.Hover,
                Placement = PopoverPlacements.Top,
                TargetElemantFilter = this.DockIconId,
            });
        }

        if (pendingRefreshWindows)
        {
            await this.popoverHandler.RefreshReplacement(this.DockIconPopoverContentId, this.DockIconPopoverHeaderId);
            pendingRefreshWindows = false;
        }
    }
}
