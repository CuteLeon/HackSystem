using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Toast.Handler;

public interface IToastContainer
{
    void PopToast(string title, string message, Icons icon, bool autoHide = true, int hideDelay = 3000);
}
