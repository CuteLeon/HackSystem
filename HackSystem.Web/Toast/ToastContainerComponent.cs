using System;
using HackSystem.Web.Toast.Model;
using HackSystem.Web.Toast.Handler;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Toast
{
    /// <summary>
    /// Toast container component
    /// </summary>
    /// <remarks> Should works with blazor.toast.js </remarks>
    public partial class ToastContainerComponent : IToastContainer, IDisposable
    {
        private DotNetObjectReference<IToastContainer> interopReference;
        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Toasts.Clear();
                    interopReference.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
