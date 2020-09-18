using System;
using System.Collections.Generic;
using HackSystem.Web.Menu.Model;

namespace HackSystem.Web.Menu.Handler
{
    public interface IMenuContext
    {
        List<MenuItem> MenuItems { get; set; }

        event EventHandler<MenuItemEventArgs> MenuItemEvent;

        void RaiseMenuItemEvent(string menuIdentity, object value);
    }
}