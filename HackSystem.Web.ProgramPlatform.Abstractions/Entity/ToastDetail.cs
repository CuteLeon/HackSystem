using HackSystem.Web.ProgramPlatform.Abstractions.Enums;

namespace HackSystem.Web.ProgramPlatform.Abstractions.Entity;

public class ToastDetail
{
    public DateTime CreateTime { get; protected set; } = DateTime.Now;

    public string Id { get; protected set; } = $"toast_{Guid.NewGuid():N}";

    public string Title { get; set; } = "Hack System";

    public string Message { get; set; } = "Hack System Toast Message.";

    public ToastIcons Icon { get; set; }

    public bool AutoHide { get; set; } = true;

    public int HideDelay { get; set; } = 3000;
}
