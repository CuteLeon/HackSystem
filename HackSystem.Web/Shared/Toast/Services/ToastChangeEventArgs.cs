using System;

namespace HackSystem.Web.Shared.Toast.Services
{
    public class ToastChangeEventArgs : EventArgs
    {
        public enum ToastEventTypes
        {
            Pop = 0,
            Hide = 1,
        }

        public ToastEventTypes EventType { get; set; }

        public ToastDetail ToastDetail { get; set; }
    }
}
