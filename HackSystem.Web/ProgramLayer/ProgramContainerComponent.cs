using System;
using System.Threading.Tasks;
using HackSystem.Web.ProgramSDK.ProgramComponent.ProgramMessages;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramLayer
{
    /// <summary>
    /// 程序容器组件
    /// </summary>
    /// <remarks>
    /// 引用外部 .Net Core Razor Class Library 项目中的 Razor 组件
    /// 详细请参考：https://github.com/dotnet/aspnetcore/issues/26228
    /// </remarks>
    public partial class ProgramContainerComponent : IDisposable
    {
        private bool disposedValue;

        protected async override Task OnInitializedAsync()
        {
            this.programLaunchSubcriber.HandleMessage = this.HandleProgramLaunchMessage;
            this.programCloseSubscriber.HandleMessage = this.HandleProgramCloseMessage;

            await base.OnInitializedAsync();
        }

        private async Task HandleProgramLaunchMessage(ProgramLaunchMessage message)
        {
            this.logger.LogInformation($"程序层容器接收到消息，重新渲染...");
            this.StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task HandleProgramCloseMessage(ProgramCloseMessage arg)
        {
            this.logger.LogInformation($"程序层容器接收到消息，重新渲染...");
            this.StateHasChanged();
            await Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.programLaunchSubcriber.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
