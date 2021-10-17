using HackSystem.Web.Component.Contracts;

namespace HackSystem.Web.ProgramPlatform.Windows.ProgramWindow;

public class ProgramWindowStyle
{
    public int? Top { get; set; }

    public int? Left { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public Borders Border { get; set; } = Borders.Border;

    public Colors BorderColor { get; set; } = Colors.Primary;

    public Colors BackgroundColor { get; set; } = Colors.White;

    public Colors CaptionBackgroundColor { get; set; } = Colors.Light;

    public Colors CaptionTextColor { get; set; } = Colors.Dark;
}
