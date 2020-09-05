﻿using System;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Program;
using static HackSystem.Web.Shared.Toast.ToastComponent;

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
            await this.GetToastContainer().PopToastAsync("测试", "测试弹窗", Icons.Warning, true, 3000);
        }
    }
}
