using System;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Program;
using static HackSystem.Web.Shared.Toast.ToastDetail;

namespace HackSystem.Web.Pages
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

        private async Task GetAll()
        {
            var result = await this.basicProgramService.GetAll();
            this.programInfo = string.Join(";", result.Select(x => $"{x.Id} ({x.Name})"));
        }

        private async Task Update()
        {
            var programs = await this.basicProgramService.GetAll();
            if (!programs.Any()) return;
            var payload = new UpdateBasicProgramDTO() { Id = programs.First().Id, Name = $"修改-{DateTime.Now}" };
            var result = await this.basicProgramService.Update(payload);
            this.programInfo = $"{result?.Id} ({result?.Name})";
        }

        private async Task Delete()
        {
            var programs = await this.basicProgramService.GetAll();
            if (!programs.Any()) return;
            var result = await this.basicProgramService.Delete(programs.First().Id);
            this.programInfo = $"{result?.Id} ({result?.Name})";
        }

        private async Task Create()
        {
            var program = new CreateBasicProgramDTO() { Name = $"新程序-{DateTime.Now}" };
            var result = await this.basicProgramService.Create(program);
            this.programInfo = $"{result.Id} ({result.Name})";
        }

        private async Task Get()
        {
            var programs = await this.basicProgramService.GetAll();
            if (!programs.Any()) return;
            var result = await this.basicProgramService.Get(programs.First().Id);
            this.programInfo = $"{result.Id} ({result.Name})";
        }

        private async Task RefreshTokenInfo()
        {
        }

        private async Task PopToast()
        {
            this.GetToastContainer().PopToast("Info 提示信息", "提示信息将在三秒内关闭", Icons.Information, true, 3000);
            this.GetToastContainer().PopToast("HackSystem 信息", "HackSystem 信息将在五秒内关闭", Icons.HackSystem, true, 5000);
            this.GetToastContainer().PopToast("Error 错误信息", "错误信息不会自动关闭", Icons.Error, false);
        }
    }
}
