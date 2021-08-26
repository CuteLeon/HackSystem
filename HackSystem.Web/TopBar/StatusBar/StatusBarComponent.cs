using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HackSystem.Web.TopBar.StatusBar;

    public partial class StatusBarComponent
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await this.jsRuntime.InvokeVoidAsync("tooltips.initTooltips");
        }
    }
