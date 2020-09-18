using System;

namespace HackSystem.Web.Menu.Model
{
    public class MenuItemEventArgs : EventArgs
    {
        public string MenuIdentity { get; set; }

        public object Value { get; set; }
    }
}
