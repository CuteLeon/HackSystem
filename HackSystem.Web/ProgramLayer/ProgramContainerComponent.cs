using System.Linq;
using HackSystem.Web.Desktop.InfoPanel;
using Microsoft.AspNetCore.Components;

namespace HackSystem.Web.ProgramLayer
{
    public partial class ProgramContainerComponent : ComponentBase
    {
        RenderFragment renderFragment = builder =>
        {
            foreach (var programID in Enumerable.Range(1, 3))
            {
                // 差分算法性能：Region 的序列号必须与程序ID对应
                builder.OpenRegion(programID);
                // 差分算法稳定性：Region 内的序列号可以重新开始递增
                builder.OpenComponent(0, typeof(MainInfoPanelComponent));
                // 为组件设置参数
                // builder.AddAttribute(1, default, default);
                // 差分算法性能：Component 的 @key 必须与程序ID对应
                builder.SetKey(programID);
                // 差分算法要求：标签开启和关闭必须对齐
                builder.CloseComponent();
                builder.CloseRegion();
            }
        };
    }
}
