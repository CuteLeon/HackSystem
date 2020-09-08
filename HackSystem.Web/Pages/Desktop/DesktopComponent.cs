using System;
using System.Linq;
using System.Threading.Tasks;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Pages.Desktop
{
    public partial class DesktopComponent
    {
        public DesktopComponent()
        {
        }

        private async Task OnTest()
        {
            try
            {
                var maps = await this.basicProgramService.QueryUserBasicProgramMaps();
                this.BasicProgramMaps = maps;
                this.GetToastContainer().PopToast("获取程序映射成功", $"获取程序映射信息成功！共 {maps.Count()} 个。", Icons.Information, true, 3000);
                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                this.GetToastContainer().PopToast("获取程序映射失败", $"获取程序映射失败:{ex.Message}", Icons.Error, false);
            }
        }
    }
}
