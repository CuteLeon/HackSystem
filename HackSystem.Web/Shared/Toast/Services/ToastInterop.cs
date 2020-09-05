using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HackSystem.Web.Shared.Toast.Services;
using Microsoft.JSInterop;
using static HackSystem.Web.Shared.Toast.Services.ToastChangeEventArgs;

namespace HackSystem.Web.Shared.Toast
{
    public static class ToastInterop
    {
        public static event EventHandler<ToastChangeEventArgs> ToastChange;

        private static Dictionary<string, Dictionary<string, ToastDetail>> Toasts = new Dictionary<string, Dictionary<string, ToastDetail>>();

        public static IReadOnlyDictionary<string, ToastDetail> GetToastCollection(string containerId)
        {
            return new ReadOnlyDictionary<string, ToastDetail>(ToastInterop.Toasts.GetValueOrDefault(containerId));
        }

        public static void CreateToastCollection(string containerId)
        {
            ToastInterop.Toasts.Add(containerId, new Dictionary<string, ToastDetail>());
        }

        public static void RemoveToastCollection(string containerId)
        {
            ToastInterop.Toasts.GetValueOrDefault(containerId)?.Clear();
            ToastInterop.Toasts.Remove(containerId);
        }

        public static void PopToast(ToastDetail toast)
        {
            ToastInterop.Toasts.GetValueOrDefault(toast.ContainerId)?.Add(toast.Id, toast);
            ToastInterop.ToastChange?.Invoke(toast.ContainerId, new ToastChangeEventArgs() { EventType = ToastEventTypes.Pop, ToastDetail = toast });
        }

        [JSInvokable]
        public static void CloseToast(string containerId, string toastId)
        {
            var toast = ToastInterop.Toasts.GetValueOrDefault(containerId)?.GetValueOrDefault(toastId);
            if (toast == null) return;

            ToastInterop.Toasts[containerId].Remove(toastId);
            ToastInterop.ToastChange?.Invoke(toast.ContainerId, new ToastChangeEventArgs() { EventType = ToastEventTypes.Hide, ToastDetail = toast });
        }
    }
}
