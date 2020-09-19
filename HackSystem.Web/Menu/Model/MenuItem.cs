using System;
using System.Collections.Generic;

namespace HackSystem.Web.Menu.Model
{
    public class MenuItem
    {
        public enum MenuTypes
        {
            Button = 0,
            CheckBox = 1,
            Radio = 2,
            Divider = 3,
        }

        public MenuItem(
            string menuIdentity = null,
            string title = null,
            MenuTypes menuType = MenuTypes.Button,
            string icon = null,
            bool active = false,
            bool enabled = true,
            bool visible = true,
            bool @checked = false,
            string radioGroup = null)
        {
            this.MenuIdentity = menuIdentity;
            this.Key = Guid.NewGuid().ToString();
            this.MenuType = menuType;
            this.Title = title;
            this.Icon = icon;
            this.Active = active;
            this.Enabled = enabled;
            this.Visible = visible;
            this.Checked = @checked;
            this.RadioGroup = radioGroup;
        }

        public string Key { get; init; }

        public string MenuIdentity { get; init; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public bool Active { get; set; }

        public bool Enabled { get; set; }

        public bool Visible { get; set; }

        public bool Checked { get; set; }

        public string RadioGroup { get; set; }

        public MenuTypes MenuType { get; init; }

        /// <remarks>
        /// 此成员为非空引用时将忽略 MenuType 成员的值，而强制使此菜单项为子菜单
        /// 允许此成员为非空引用单不包含任何元素，以实现空的子菜单
        /// </remarks>
        public List<MenuItem> SubMenuItems { get; set; }

        public MenuItem AddSubMenus(params MenuItem[] subMenuItems)
        {
            if (this.SubMenuItems == null)
                this.SubMenuItems = new List<MenuItem>();

            this.SubMenuItems.AddRange(subMenuItems);
            return this;
        }
    }
}
