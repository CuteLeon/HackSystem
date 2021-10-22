namespace HackSystem.Web.Component.Popover;

public class PopoverDetail
{
    public string TargetElemantFilter { get; set; }

    public string Title { get; set; } = "Hack System";

    public bool IsHtmlContent { get; set; }

    public string Content { get; set; } = "Hack System Popover Content.";

    public PopoverTriggers Trigger { get; set; } = PopoverTriggers.Hover;

    public PopoverPlacements Placement { get; set; } = PopoverPlacements.Top;

    public int Offset { get; set; }

    public int ShowDelay { get; set; }

    public int HideDelay { get; set; }

    public bool ReplaceContent { get; set; } = false;
}
