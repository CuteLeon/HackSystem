using HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository.ProgramAssets;

public interface IProgramAssetService
{
    Task<ProgramAssetPackage> QueryProgramAssetList(string programId);

    Task<byte[]> QueryProgramIcon(string programId);

    Task<ProgramAssetPackage> QueryProgramAssetPackage(string programId);

    Task<ProgramAssetPackage> QueryProgramAssetPackage(ProgramAssetPackage package);
}
