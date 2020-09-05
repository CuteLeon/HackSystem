using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Shared.Toast
{
    public interface IToastContainer
    {
        void PopToast(string title, string message, Icons icon, bool autoHide = true, int hideDelay = 3000);
    }
}
