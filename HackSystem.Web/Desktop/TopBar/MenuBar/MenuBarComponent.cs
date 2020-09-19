using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HackSystem.Web.Desktop.TopBar.MenuBar
{
    public partial class MenuBarComponent
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await this.jsRuntime.InvokeVoidAsync("submenus.initSubMenus");
        }
    }
}
