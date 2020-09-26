using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSDK.ProgramComponent;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramDrawer
{
    public partial class ProgramDrawerComponent
    {
        public void ClearProgramDrawer()
        {
            this.BasicProgramMaps.Clear();
            this.StateHasChanged();
        }

        public void LoadProgramDrawer(IEnumerable<QueryUserBasicProgramMapDTO> maps)
        {
            this.BasicProgramMaps.Clear();

            foreach (var map in maps.OrderByDescending(map => map.PinToTop))
            {
                this.BasicProgramMaps.Add(map.BasicProgram.Id, map);
            }
            this.StateHasChanged();
        }

        public async Task OnDoubleClickIcon(ProgramDrawerIconMouseEventArgs args)
        {
            var assemblyName = args.UserBasicProgramMap.BasicProgram.AssemblyName;
            var typeName = args.UserBasicProgramMap.BasicProgram.TypeName;
            this.logger.LogWarning(typeName);

            if (string.IsNullOrWhiteSpace(assemblyName) ||
               string.IsNullOrWhiteSpace(typeName))
            {
                return;
            }

            try
            {
                var assembly = Assembly.Load(new AssemblyName(assemblyName));
                if (assembly == null)
                {
                    this.logger.LogWarning($"使用方法二获取程序集为空");
                }
                this.logger.LogWarning($"使用方法二获取程序集：{assembly.FullName}");

                var type = assembly.GetType(typeName);
                if (type == null)
                {
                    this.logger.LogWarning($"使用方法二获取类型为空");
                }
                this.logger.LogWarning($"使用方法二获取类型：{type.FullName}");

                if (!typeof(ProgramComponentBase).IsAssignableFrom(type))
                {
                    throw new TypeLoadException($"The target program type must derive from {typeof(ProgramComponentBase).Name}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, $"使用方法二获取类型失败:");
            }
        }
    }
}
