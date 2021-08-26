using Microsoft.JSInterop;

namespace HackSystem.Web.Desktop;

public partial class DesktopMenuComponent
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await this.jsRuntime.InvokeVoidAsync("submenus.initSubMenus");
    }
}
