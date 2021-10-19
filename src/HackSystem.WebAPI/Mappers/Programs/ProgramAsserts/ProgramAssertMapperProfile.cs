using HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;
using HackSystem.DataTransferObjects.Programs.ProgramAssets;

namespace HackSystem.WebAPI.Mappers.ProgramMapper.ProgramAsserts;

public class ProgramAssertMapperProfile : Profile
{
    public ProgramAssertMapperProfile()
    {
        this.CreateMap<ProgramAsset, ProgramAssetResponse>();
        this.CreateMap<ProgramAssetRequest, ProgramAsset>();
        this.CreateMap<ProgramAssetPackage, ProgramAssetPackageResponse>();
        this.CreateMap<ProgramAssetPackageRequest, ProgramAssetPackage>();
    }
}
