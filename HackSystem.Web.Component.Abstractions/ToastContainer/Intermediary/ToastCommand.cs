using HackSystem.Intermediary.Domain;

namespace HackSystem.Web.Component.ToastContainer;

public class ToastEvent : IIntermediaryEvent
{
    public ToastDetail ToastDetail { get; set; }
}
