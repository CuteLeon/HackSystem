using System.Threading.Tasks;

namespace HackSystem.Web.Shared.Toast
{
    public interface IToastContainer
    {
        Task PopToastAsync(string title, string message, ToastComponent.Icons icon, bool autoHide = true, int hideDelay = 3000);
    }
}
