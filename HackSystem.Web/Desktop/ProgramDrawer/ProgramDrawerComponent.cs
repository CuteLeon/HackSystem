using System.Collections.Generic;
using System.Linq;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.Desktop.ProgramDrawer
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
    }
}
