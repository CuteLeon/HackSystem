using System.Threading.Tasks;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Shared.Toast
{
    public interface IToastContainer
    {
        Task PopToastAsync(string title, string message, Icons icon, bool autoHide = true, int hideDelay = 3000);
    }
}
