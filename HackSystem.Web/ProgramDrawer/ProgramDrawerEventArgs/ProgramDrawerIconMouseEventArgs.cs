using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

    public class ProgramDrawerIconMouseEventArgs : MouseEventArgs
    {
        public QueryUserBasicProgramMapDTO UserBasicProgramMap { get; set; }
    }
