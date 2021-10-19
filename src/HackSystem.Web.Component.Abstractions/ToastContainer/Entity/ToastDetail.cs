namespace HackSystem.Web.Component.ToastContainer;

public class ToastDetail
{
    public DateTime CreateTime { get; init; } = DateTime.Now;

    public string Id { get; init; } = $"toast_{Guid.NewGuid():N}";

    public string Title { get; set; } = "Hack System";

    public string Message { get; set; } = "Hack System Toast Message.";

    public ToastIcons Icon { get; set; } = ToastIcons.HackSystem;

    public bool AutoHide { get; set; } = true;

    public int HideDelay { get; set; } = 3000;
}
