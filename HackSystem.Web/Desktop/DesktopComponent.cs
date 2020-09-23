using System;
using System.Linq;
using System.Threading.Tasks;
using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Desktop
{
    public partial class DesktopComponent
    {
        public DesktopComponent()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private void OnTest()
        {
            this.GetDesktopToastContainer().PopToast("Hack System", "此消息将在 1 秒自动关闭。", Icons.Information, true, 1000);
            this.GetDesktopToastContainer().PopToast("Hack System", "此消息将在 2 秒自动关闭。", Icons.Question, true, 2000);
            this.GetDesktopToastContainer().PopToast("Hack System", "此消息将在 3 秒自动关闭。", Icons.Warning, true, 3000);
            this.GetDesktopToastContainer().PopToast("Hack System", "此消息需要处理后手动关闭。", Icons.Error, false);
            this.GetDesktopToastContainer().PopToast("Hack System", "欢迎进入 Hack System。", Icons.HackSystem, false);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await this.LoadProgramIcons();
                this.StateHasChanged();
            }
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
                            map.BasicProgram.IconUrl = new Uri(new Uri(this.apiConfiguration.CurrentValue.APIURL), map.BasicProgram.IconUrl).AbsoluteUri;
                            return map;
                        })
                        .ToDictionary(map => map.BasicProgram.Id, map => map);

                    this.ProgramDrawerComponent.LoadProgramDrawer(maps);
                    this.ProgramDockComponent.LoadProgramDock(maps.Where(map => map.PinToDock));
                }
                else
                {
                    this.ProgramDockComponent.ClearProgramDock();
                    this.ProgramDrawerComponent.ClearProgramDrawer();
                    this.BasicProgramMaps.Clear();
                }
                this.GetDesktopToastContainer().PopToast("获取程序映射成功", $"获取程序映射信息成功！共 {this.BasicProgramMaps.Count} 个。", Icons.Information, true, 3000);
            }
            catch (Exception ex)
            {
                this.GetDesktopToastContainer().PopToast("获取程序映射失败", $"获取程序映射失败:{ex.Message}", Icons.Error, false);
            }
        }
    }
}
