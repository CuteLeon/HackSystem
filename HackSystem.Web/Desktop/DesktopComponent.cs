using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.Web.Menu.Handler;
using HackSystem.Web.Menu.Model;
using Microsoft.Extensions.Logging;
using static HackSystem.Web.Menu.Model.MenuItem;
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
            this.MenuContext.MenuItems = new List<MenuItem>
            {
                new MenuItem("File", "文件").AddSubMenus(
                    new MenuItem("NewFile", "新文件"),
                    new MenuItem("OpenFile", "打开文件"),
                    new MenuItem("SaveFile", "保存文件"),
                    new MenuItem("CloseFile", "关闭文件")),
                new MenuItem("Edit", "编辑").AddSubMenus(
                    new MenuItem("Cut", "剪切"),
                    new MenuItem("Copy", "复制"),
                    new MenuItem("Paste", "粘贴"),
                    new MenuItem("Delete", "删除"),
                    new MenuItem(menuType: MenuTypes.Divider),
                    new MenuItem("Select", "选择").AddSubMenus(
                        new MenuItem("SelectAll", "全选"),
                        new MenuItem("SelectNone", "全不选"),
                        new MenuItem("SelectInvert", "反选")),
                    new MenuItem(menuType: MenuTypes.Divider),
                    new MenuItem("Find", "查找"),
                    new MenuItem("Replace", "替换")
                    ),
                new MenuItem("View", "视图").AddSubMenus(
                    new MenuItem("TopBar", "顶部栏", MenuTypes.CheckBox),
                    new MenuItem("MenuBar", "菜单栏", MenuTypes.CheckBox),
                    new MenuItem("StatusBar", "状态栏", MenuTypes.CheckBox),
                    new MenuItem(menuType: MenuTypes.Divider),
                    new MenuItem("ProgramDock", "程序坞", MenuTypes.CheckBox)),
                new MenuItem("Test","测试")
            };

            this.MenuContext.MenuItemEvent += this.MenuContext_MenuItemEventHandler;

            await base.OnInitializedAsync();
        }

        private void MenuContext_MenuItemEventHandler(object sender, MenuItemEventArgs e)
        {
            this.logger.LogInformation($"Desktop menu event: {nameof(e.MenuIdentity)}=>{e.MenuIdentity}, {nameof(e.Value)}=>{e.Value}");

            switch (e.MenuIdentity)
            {
                case "Test":
                    {
                        this.OnTest();
                        break;
                    }
                default:
                    break;
            }
        }

        private void OnTest()
        {
            this.GetToastContainer().PopToast("Hack System", "此消息将在 1 秒自动关闭。", Icons.Information, true, 1000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 2 秒自动关闭。", Icons.Question, true, 2000);
            this.GetToastContainer().PopToast("Hack System", "此消息将在 3 秒自动关闭。", Icons.Warning, true, 3000);
            this.GetToastContainer().PopToast("Hack System", "此消息需要处理后手动关闭。", Icons.Error, false);
            this.GetToastContainer().PopToast("Hack System", "欢迎进入 Hack System。", Icons.HackSystem, false);
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
