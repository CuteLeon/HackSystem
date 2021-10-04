using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.Mappers.ProgramDrawerMappers;

public class ProgramDrawerIconMouseEventArgsMapper : Profile
{
    public ProgramDrawerIconMouseEventArgsMapper()
    {
        this.CreateMap<MouseEventArgs, ProgramIconMouseEventArgs>();
    }
}
