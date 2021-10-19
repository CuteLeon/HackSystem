using System.Text;
using HackSystem.Web.Component.Contracts;

namespace HackSystem.Web.Component.Extensions;

public static class BordersExtension
{
    public static string ToBorderStyle(this Borders border)
    {
        if (border == Borders.None) return "border-0";
        else if (border == Borders.Border) return "border";
        else
        {
            var borderStyleBuilder = new StringBuilder(64);
            if (!border.HasFlag(Borders.BorderTop)) borderStyleBuilder.Append("border-top-0 ");
            if (!border.HasFlag(Borders.BorderBottom)) borderStyleBuilder.Append("border-bottom-0 ");
            if (!border.HasFlag(Borders.BorderLeft)) borderStyleBuilder.Append("border-left-0 ");
            if (!border.HasFlag(Borders.BorderRight)) borderStyleBuilder.Append("border-right-0 ");
            return borderStyleBuilder.ToString();
        }
    }
}
