using HackSystem.Web.Component.Contracts;

namespace HackSystem.Web.ProgramPlatform.Windows.ProgramWindow;

public class ProgramWindowStyle
{
    public string Top { get; set; } = "5%";

    public string Left { get; set; } = "25%";

    public string Width { get; set; } = "50%";

    public string Height { get; set; } = "75%";

    public Borders Border { get; set; } = Borders.Border;

    public Colors BorderColor { get; set; } = Colors.Light;

    public Colors BackgroundColor { get; set; } = Colors.White;

    public Colors CaptionBackgroundColor { get; set; } = Colors.Light;

    public Colors CaptionTextColor { get; set; } = Colors.Dark;
}
