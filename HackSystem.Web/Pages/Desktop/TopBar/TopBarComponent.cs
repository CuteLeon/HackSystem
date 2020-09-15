using System.Threading.Tasks;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Pages.Desktop.TopBar
{
    public partial class TopBarComponent
    {
        private async Task OnTest()
        {
            this.GetToastContainer().PopToast("Hack System", "此消息将在 1 秒自动关闭。", Icons.Information, true, 1000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 2 秒自动关闭。", Icons.Question, true, 2000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 3 秒自动关闭。", Icons.Warning, true, 3000);
            this.GetToastContainer().PopToast("Hack System", "此消息需要处理后手动关闭。", Icons.Error, false);
            this.GetToastContainer().PopToast("Hack System", "欢迎进入 Hack System。", Icons.HackSystem, false);
        }
    }
}
