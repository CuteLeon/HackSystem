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

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await this.LoadProgramIcons();
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
                            map.BasicProgram.IconUrl = new Uri(new Uri(apiConfiguration.CurrentValue.APIURL), map.BasicProgram.IconUrl).AbsoluteUri;
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
