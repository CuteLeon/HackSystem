using System.Collections.Generic;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.Pages.Desktop.ProgramDock
{
    public partial class ProgramDockComponent
    {
        public void ClearProgramDock()
        {
            this.BasicProgramMaps.Clear();
            this.StateHasChanged();
        }

        public void LoadProgramDock(IEnumerable<QueryUserBasicProgramMapDTO> maps)
        {
            this.BasicProgramMaps.Clear();

            foreach (var map in maps)
            {
                this.BasicProgramMaps.Add(map.BasicProgram.Id, map);
            }
            this.StateHasChanged();
        }
    }
}
