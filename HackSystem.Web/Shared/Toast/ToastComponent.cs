using System.Threading.Tasks;
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
                // TODO: Leon: Toast 组件自动关闭之后，需要手动从 ToastSet 移除并自动 Dispose：监听 hide 事件？
                await this.jsRuntime.InvokeVoidAsync("toasts.popToast", this.ToastDetail.Id, this.ToastDetail.AutoHide, this.ToastDetail.HideDelay);
            }
        }
    }
}
