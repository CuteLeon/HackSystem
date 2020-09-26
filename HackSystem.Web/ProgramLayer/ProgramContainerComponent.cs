using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
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
    public partial class ProgramContainerComponent
    {
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            this.programContainer.OnProcessesUpdate = new EventCallback(this, (Action)(() =>
            {
                this.logger.LogInformation($"程序层容器接收到事件，重新渲染...");
                this.StateHasChanged();
            }));
        }
    }
}
