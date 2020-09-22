using System.Collections.Generic;
using System.Linq;
using HackSystem.Web.InfoPanel;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramLayer
{
    public partial class ProgramContainerComponent
    {
        Dictionary<int, ComponentBase> programComponents = new Dictionary<int, ComponentBase>();

        RenderFragment renderFragment;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.logger.LogInformation("初始化...");
            this.renderFragment = builder =>
            {
                foreach (var programID in Enumerable.Range(1, 3))
                {
                    // 差分算法性能：Region 的序列号必须与程序ID对应
                    builder.OpenRegion(programID);
                    // 差分算法稳定性：Region 内的序列号可以重新开始递增
                    builder.OpenComponent(0, typeof(MainInfoPanelComponent));
                    builder.AddComponentReferenceCapture(1, reference =>
                    {
                        this.logger.LogInformation($"捕捉引用：{programID}");
                        programComponents.Add(programID, (ComponentBase)reference);
                    });
                    // 为组件设置参数
                    // builder.AddAttribute(1, default, default);
                    // 差分算法性能：Component 的 @key 必须与程序ID对应
                    builder.SetKey(programID);
                    // 差分算法要求：标签开启和关闭必须对齐
                    builder.CloseComponent();
                    builder.CloseRegion();
                }
            };
            
            // 按引用操作程序组件实例
            // programComponents.FirstOrDefault().Value.SetParametersAsync();
        }
    }
}
