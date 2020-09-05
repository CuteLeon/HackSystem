using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace HackSystem.Web.Shared.Toast
{
    public partial class ToastComponent
    {
        public ToastComponent()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                this.logger.LogInformation($"Initialize Toast: {this.ToastDetail.Title}({this.ToastDetail.Id})");
                await this.jsRuntime.InvokeVoidAsync("toasts.popToast", this.ToastDetail.Id, this.ToastDetail.AutoHide, this.ToastDetail.HideDelay);
            }
        }
    }
}
