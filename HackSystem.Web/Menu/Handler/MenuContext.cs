using System;
using System.Collections.Generic;
using HackSystem.Web.Menu.Model;

namespace HackSystem.Web.Menu.Handler
{
    public class MenuContext : IMenuContext
    {
        public List<MenuItem> MenuItems { get; set; }

        public event EventHandler<MenuItemEventArgs> MenuItemEvent;

        public void RaiseMenuItemEvent(string menuIdentity, object value)
            => this.MenuItemEvent?.Invoke(this, new MenuItemEventArgs() { MenuIdentity = menuIdentity, Value = value });
    }
}
