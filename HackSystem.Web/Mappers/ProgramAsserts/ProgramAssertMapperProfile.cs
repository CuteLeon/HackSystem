using HackSystem.DataTransferObjects.Programs.ProgramAssets;

namespace HackSystem.Web.Mappers.ProgramAsserts;

public class ProgramAssertMapperProfile : Profile
{
    public ProgramAssertMapperProfile()
    {
        this.CreateMap<ProgramAssetPackageRequest, ProgramAssetPackageRequest>();
        this.CreateMap<ProgramAssetPackageResponse, ProgramAssetPackageResponse>();
    }
}
