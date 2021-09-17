namespace HackSystem.Web.Toast.Model;

public class ToastDetail
{
    public enum Icons
    {
        HackSystem = 0,
        Information = 1,
        Question = 2,
        Warning = 3,
        Error = 4
    }

    public DateTime CreateTime { get; protected set; } = DateTime.Now;

    public string Id { get; protected set; } = $"toast_{Guid.NewGuid():N}";

    public string Title { get; set; } = "Hack System";

    public string Message { get; set; } = "Hack System Toast Message.";

    public Icons Icon { get; set; }

    public bool AutoHide { get; set; } = true;

    public int HideDelay { get; set; } = 3000;
}
