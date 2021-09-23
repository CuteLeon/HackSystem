using HackSystem.DataTransferObjects.Programs.ProgramAssets;

namespace HackSystem.Web.Mappers.ProgramAsserts;

public class ProgramAssertMapperProfile : Profile
{
    public ProgramAssertMapperProfile()
    {
        this.CreateMap<ProgramAssetRequest, ProgramAssetResponse>();
        this.CreateMap<ProgramAssetResponse, ProgramAssetRequest>();
        this.CreateMap<ProgramAssetPackageRequest, ProgramAssetPackageResponse>();
        this.CreateMap<ProgramAssetPackageResponse, ProgramAssetPackageRequest>();
    }
}
