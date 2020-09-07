using System.Threading.Tasks;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Pages.Desktop
{
    public partial class DesktopComponent
    {
        public DesktopComponent()
        {
        }

        private async Task GetAccountInfo()
        {
            accountInfo = await this.authenticationService.GetAccountInfo();
        }

        private async Task PopToast()
        {
            this.GetToastContainer().PopToast("Info 提示信息", "提示信息将在三秒内关闭", Icons.Information, true, 3000);
            this.GetToastContainer().PopToast("HackSystem 信息", "HackSystem 信息将在五秒内关闭", Icons.HackSystem, true, 5000);
            this.GetToastContainer().PopToast("Error 错误信息", "错误信息不会自动关闭", Icons.Error, false);
        }
    }
}
