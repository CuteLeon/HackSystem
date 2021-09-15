using System.Threading.Tasks;

namespace HackSystem.WebAPI.Services.API.Program.ProgramAsset;

public interface IProgramAssetService
{
    Task<ProgramAssetPackage> QueryProgramAssetList(string programId);

    Task<ProgramAssetPackage> QueryProgramAssetPackage(string programId);

    Task<ProgramAssetPackage> QueryProgramAssetPackage(ProgramAssetPackage package);
}
