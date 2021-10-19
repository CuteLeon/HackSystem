using HackSystem.Web.Component.Contracts;

namespace HackSystem.Web.Component.Extensions;

public static class ColorsExtension
{
    public static string ToTextColor(this Colors color)
        => $"text-{color.ToGenericColor()}";

    public static string ToBackgroundColor(this Colors color)
        => $"bg-{color.ToGenericColor()}";

    public static string ToBorderColor(this Colors color)
        => color switch
        {
            Colors.None => "border-0",
            Colors.Transparent => "border-0",
            _ => $"border-{color.ToGenericColor()}"
        };

    public static string ToGenericColor(this Colors color)
        => color switch
        {
            Colors.Primary => "primary",
            Colors.Secondary => "secondary",
            Colors.Success => "success",
            Colors.Danger => "danger",
            Colors.Warning => "warning",
            Colors.Info => "info",
            Colors.Light => "light",
            Colors.Dark => "dark",
            Colors.White => "white",
            Colors.None => "transparent",
            Colors.Transparent => "transparent"
        };
}
