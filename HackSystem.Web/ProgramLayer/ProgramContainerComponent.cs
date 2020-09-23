using System.Collections.Generic;
using System.Linq;
using HackSystem.Web.ProgramLayer.ProgramComponent;
using HackSystem.Web.SystemProgram;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramLayer
{
    public partial class ProgramContainerComponent
    {
        private readonly Dictionary<int, ComponentBase> programComponents = new Dictionary<int, ComponentBase>();

        RenderFragment renderFragment;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.logger.LogInformation("初始化...");
            this.renderFragment = builder =>
            {
                _ = new[] {
                    typeof(Program1Component),
                    typeof(Program2Component),
                    typeof(Program3Component),
                    typeof(Program4Component),
                    typeof(Program5Component),
                    typeof(SystemProgram1Component)
                }.Select((programType, index) =>
                {
                    var programID = index;
                    var programComponent = default(ProgramComponentBase);
                    var programEntity = new ProgramEntity()
                    {
                        PID = programID,
                        Z_Index = programID,
                    };

                    // 差分算法性能：Region 的序列号必须与程序ID对应
                    builder.OpenRegion(programID);
                    // 差分算法稳定性：Region 内的序列号可以重新开始递增
                    builder.OpenComponent(0, programType);
                    // Attribute 需要先于其他数据被添加
                    builder.AddAttribute(2, nameof(ProgramComponentBase.ProgramEntity), programEntity);
                    builder.AddComponentReferenceCapture(1, reference =>
                    {
                        programComponent = (ProgramComponentBase)reference;
                        programComponents.Add(programID, programComponent);
                    });
                    // 为组件设置参数
                    // builder.AddAttribute(1, default, default);
                    // 差分算法性能：Component 的 @key 必须与程序ID对应
                    builder.SetKey(programID);
                    // 差分算法要求：标签开启和关闭必须对齐
                    builder.CloseComponent();
                    builder.CloseRegion();

                    // 按引用操作程序组件实例
                    // await programComponent.SetParametersAsync(default);
                    return programComponent;
                }).ToArray();
            };
        }
    }
}
