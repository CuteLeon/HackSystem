using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace HackSystem.Web.Toast
{
    public partial class ToastComponent
    {
        public ToastComponent()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this.logger.LogDebug($"After render Toast: {this.ToastDetail.Title}({this.ToastDetail.Id})");
            await base.OnAfterRenderAsync(firstRender);

            // MAGIC! DO NOT TOUCH! This component is losing the status which from Bootstrap's initial method after each time renderred, have to re-initial after that.
            await this.jsRuntime.InvokeVoidAsync("toasts.popToast", this.ToastContainerInterop, this.ToastDetail.Id, this.ToastDetail.AutoHide, this.ToastDetail.HideDelay);
        }
    }
}
