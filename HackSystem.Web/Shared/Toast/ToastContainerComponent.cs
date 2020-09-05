using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using static HackSystem.Web.Shared.Toast.ToastComponent;

namespace HackSystem.Web.Shared.Toast
{
    /// <summary>
    /// Toast 容器组件
    /// </summary>
    /// <remarks> 需要配合 blazor.toast.js 使用 </remarks>
    public partial class ToastContainerComponent : IToastContainer
    {
        // TODO Leon: 以通过级联参数将此方法下发给子元素
        public async Task PopToastAsync(string title, string message, Icons icon, bool autoHide = true, int hideDelay = 3000)
        {
            this.logger.LogWarning($"Try to pop a Toast: {title}");
            var toast = new ToastComponent()
            {
                Title = title,
                Message = message,
                Icon = icon,
                AutoHide = autoHide,
                HideDelay = hideDelay,
            };
            this.ToastSet.Add(toast);

            await this.jsRuntime.InvokeVoidAsync("toasts.popToast", toast.Id, autoHide, hideDelay);
        }
    }
}
