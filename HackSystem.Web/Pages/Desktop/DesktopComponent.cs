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

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await this.LoadProgramIcons();
            }
        }

        private async Task OnTest()
        {
            this.GetToastContainer().PopToast("Hack System", "此消息将在 1 秒自动关闭。", Icons.Information, true, 1000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 2 秒自动关闭。", Icons.Question, true, 2000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 3 秒自动关闭。", Icons.Warning, true, 3000);
            this.GetToastContainer().PopToast("Hack System", "此消息需要处理后手动关闭。", Icons.Error, false);
            this.GetToastContainer().PopToast("Hack System", "欢迎进入 Hack System。", Icons.HackSystem, false);
            await this.LoadProgramIcons();
        }

        private async Task LoadProgramIcons()
        {
            try
            {
                var maps = await this.basicProgramService.QueryUserBasicProgramMaps();
                if (maps?.Any() ?? false)
                {
                    this.BasicProgramMaps = maps
                        .Select(map =>
                        {
                            map.BasicProgram.IconUrl = new Uri(new Uri(apiConfiguration.CurrentValue.APIURL), map.BasicProgram.IconUrl).AbsoluteUri;
                            return map;
                        })
                        .ToDictionary(map => map.BasicProgram.Id, map => map);

                    this.ProgramDockComponent.LoadProgramDock(maps.Where(map => map.PinToDock));
                }
                else
                {
                    this.ProgramDockComponent.ClearProgramDock();
                    this.BasicProgramMaps.Clear();
                }
                this.GetToastContainer().PopToast("获取程序映射成功", $"获取程序映射信息成功！共 {this.BasicProgramMaps.Count()} 个。", Icons.Information, true, 3000);
                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                this.GetToastContainer().PopToast("获取程序映射失败", $"获取程序映射失败:{ex.Message}", Icons.Error, false);
            }
        }
    }
}
