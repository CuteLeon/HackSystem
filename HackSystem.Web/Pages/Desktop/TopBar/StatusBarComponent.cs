using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HackSystem.Web.Pages.Desktop.TopBar
{
    public partial class StatusBarComponent
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await this.jsRuntime.InvokeVoidAsync("statusbar.refreshTooltip");
        }
    }
}
