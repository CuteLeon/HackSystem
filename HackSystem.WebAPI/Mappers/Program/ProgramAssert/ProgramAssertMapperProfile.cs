using AutoMapper;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebDataTransfer.Program.ProgramAsset;

namespace HackSystem.WebAPI.Mappers.ProgramMapper.ProgramAssert;
public class ProgramAssertMapperProfile : Profile
{
    public ProgramAssertMapperProfile()
    {
        this.CreateMap<ProgramAsset, ProgramAssetDTO>();
        this.CreateMap<ProgramAssetDTO, ProgramAsset>();
        this.CreateMap<ProgramAssetPackage, ProgramAssetPackageDTO>();
        this.CreateMap<ProgramAssetPackageDTO, ProgramAssetPackage>();
    }
}
