namespace HackSystem.Web.Component.ToastContainer;

public interface IToastHandler
{
    Task PopupToast(ToastDetail toastDetail);
}
