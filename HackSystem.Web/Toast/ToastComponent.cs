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
            this.logger.LogDebug($"Initialize Toast: {this.ToastDetail.Title}({this.ToastDetail.Id})");
            await base.OnAfterRenderAsync(firstRender);

            // 魔法！勿动！Blazor 每次重新渲染此组件后，此组件将会丢失由 Bootstrap 初始化方法赋予的状态，每次渲染吼都必须重新初始化
            await this.jsRuntime.InvokeVoidAsync("toasts.popToast", this.ToastContainerInterop, this.ToastDetail.Id, this.ToastDetail.AutoHide, this.ToastDetail.HideDelay);
        }
    }
}
