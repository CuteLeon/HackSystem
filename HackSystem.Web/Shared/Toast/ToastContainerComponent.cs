using System;
using HackSystem.Web.Shared.Toast.Services;
using Microsoft.Extensions.Logging;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Shared.Toast
{
    /// <summary>
    /// Toast 容器组件
    /// </summary>
    /// <remarks> 需要配合 blazor.toast.js 使用 </remarks>
    public partial class ToastContainerComponent : IToastContainer, IDisposable
    {
        public ToastContainerComponent()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ToastInterop.ToastChange += this.ToastInterop_ToastChange;
            ToastInterop.CreateToastCollection(this.Id);
        }

        private void ToastInterop_ToastChange(object sender, ToastChangeEventArgs e)
        {
            if (!this.Id.Equals(e.ToastDetail.ContainerId, StringComparison.InvariantCultureIgnoreCase))
                return;

            this.StateHasChanged();
        }

        public void PopToast(string title, string message, Icons icon = Icons.HackSystem, bool autoHide = true, int hideDelay = 3000)
        {
            this.logger.LogWarning($"Pop a Toast: {title}");
            var toast = new ToastDetail()
            {
                ContainerId = this.Id,
                Title = title,
                Message = message,
                Icon = icon,
                AutoHide = autoHide,
                HideDelay = hideDelay,
            };
            ToastInterop.PopToast(toast);
            this.StateHasChanged();
        }

        public void Dispose()
        {
            ToastInterop.RemoveToastCollection(this.Id);
        }
    }
}
