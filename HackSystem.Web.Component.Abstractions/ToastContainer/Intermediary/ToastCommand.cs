using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.Component.ToastContainer.Intermediary;

public class ToastEvent : IIntermediaryEvent
{
    public string title { get; set; }

    public string message { get; set; }

    public ToastIcons icon { get; set; }

    public bool autoHide { get; set; } = true;

    public int hideDelay { get; set; } = 3000;
}
