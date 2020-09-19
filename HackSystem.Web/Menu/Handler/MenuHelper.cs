using System.Collections.Generic;
using HackSystem.Web.Menu.Model;

namespace HackSystem.Web.Menu.Handler
{
    public static class MenuHelper
    {
        public static Dictionary<string, object> GetInputAttributions(MenuItem menuItem)
        {
            var inputAttributes = new Dictionary<string, object>();
            if (!menuItem.Enabled)
                inputAttributes.Add("disabled", "disabled");
            if (menuItem.Checked)
                inputAttributes.Add("checked", "checked");
            return inputAttributes;
        }

        public static string GetInputId(MenuItem menuItem)
        {
            return $"{menuItem.MenuIdentity}{menuItem.Key}";
        }
    }
}
