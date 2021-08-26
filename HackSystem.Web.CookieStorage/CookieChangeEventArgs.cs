using System;

namespace HackSystem.Web.CookieStorage;

    public class CookieChangedEventArgs : EventArgs
    {
        public string Name { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }
    }
