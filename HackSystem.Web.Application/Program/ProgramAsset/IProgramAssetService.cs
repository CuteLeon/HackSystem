using HackSystem.DataTransferObjects.Programs.ProgramAssets;

namespace HackSystem.Web.Application.Program.ProgramAsset;

public interface IProgramAssetService
{
    Task<ProgramAssetPackageResponse> QueryProgramAssetList(string programId);

    Task<ProgramAssetPackageResponse> QueryProgramAssetPackage(ProgramAssetPackageRequest packageRequest);
}
