using HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;
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
