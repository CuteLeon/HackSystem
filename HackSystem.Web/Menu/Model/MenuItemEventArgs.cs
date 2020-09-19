using System;

namespace HackSystem.Web.Menu.Model
{
    public class MenuItemEventArgs : EventArgs
    {
        public string MenuIdentity { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
