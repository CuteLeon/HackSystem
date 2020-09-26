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
            this.logger.LogInformation($"双击启动程序：{args.UserBasicProgramMap.BasicProgram.Name}");
            this.programLauncher.LaunchProgram(args.UserBasicProgramMap.BasicProgram);
        }
    }
}
