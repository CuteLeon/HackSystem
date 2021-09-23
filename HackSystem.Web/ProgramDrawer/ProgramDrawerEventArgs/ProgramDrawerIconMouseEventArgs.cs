using HackSystem.DataTransferObjects.Programs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;

public class ProgramDrawerIconMouseEventArgs : MouseEventArgs
{
    public UserBasicProgramMapResponse UserBasicProgramMap { get; set; }
}
