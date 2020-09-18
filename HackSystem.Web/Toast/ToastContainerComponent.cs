using System;
using HackSystem.Web.Toast.Model;
using HackSystem.Web.Toast.Handler;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Toast
{
    /// <summary>
    /// Toast 容器组件
    /// </summary>
    /// <remarks> 需要配合 blazor.toast.js 使用 </remarks>
    public partial class ToastContainerComponent : IToastContainer, IDisposable
    {
        private DotNetObjectReference<IToastContainer> interopReference;

        public ToastContainerComponent()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            interopReference = DotNetObjectReference.Create<IToastContainer>(this);
        }

        public void PopToast(string title, string message, Icons icon = Icons.HackSystem, bool autoHide = true, int hideDelay = 3000)
        {
            this.logger.LogDebug($"Pop a Toast: {title}");
            var toast = new ToastDetail()
            {
                Title = title,
                Message = message,
                Icon = icon,
                AutoHide = autoHide,
                HideDelay = hideDelay,
            };

            this.Toasts.Add(toast.Id, toast);
            this.StateHasChanged();
        }

        [JSInvokable]
        public void CloseToast(string toastId)
        {
            this.Toasts.Remove(toastId);
        }

        public void Dispose()
        {
            this.Toasts.Clear();
            interopReference.Dispose();
        }
    }
}
