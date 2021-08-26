using AutoMapper;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.MapperProfile.ProgramDrawerMapper;

public class ProgramDrawerIconMouseEventArgsMapper : Profile
{
    public ProgramDrawerIconMouseEventArgsMapper()
    {
        this.CreateMap<MouseEventArgs, ProgramDrawerIconMouseEventArgs>();
    }
}
