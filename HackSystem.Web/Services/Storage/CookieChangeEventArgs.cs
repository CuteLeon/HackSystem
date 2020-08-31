using System;

namespace HackSystem.Web.Services.Storage
{
    public class CookieChangedEventArgs : EventArgs
    {
        public string Name { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }
    }
}
