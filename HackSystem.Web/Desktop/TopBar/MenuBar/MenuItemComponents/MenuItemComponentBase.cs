using HackSystem.Web.Menu.Model;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.Desktop.TopBar.MenuBar.MenuItemComponents
{
    public abstract class MenuItemComponentBase : ComponentBase
    {
        private MenuItem menuItem;

        [Parameter]
        public MenuItem MenuItem { get => menuItem; set => menuItem = value; }
    }
}
