using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Shared.Toast
{
    /// <summary>
    /// Toast 容器组件
    /// </summary>
    /// <remarks> 需要配合 blazor.toast.js 使用 </remarks>
    public partial class ToastContainerComponent : IToastContainer
    {
        public async Task PopToastAsync(string title, string message, Icons icon = Icons.HackSystem, bool autoHide = true, int hideDelay = 3000)
        {
            this.logger.LogWarning($"Try to pop a Toast: {title}");
            var toast = new ToastDetail()
            {
                Title = title,
                Message = message,
                Icon = icon,
                AutoHide = autoHide,
                HideDelay = hideDelay,
            };
            this.ToastSet.Add(toast);
            this.StateHasChanged();
        }
    }
}
