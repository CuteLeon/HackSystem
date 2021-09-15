using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Program.ProgramAsset;

namespace HackSystem.Web.Services.API.Program.ProgramAsset;

public interface IProgramAssetService
{
    Task<ProgramAssetPackageDTO> QueryProgramAssetList(string programId);

    Task<ProgramAssetPackageDTO> QueryProgramAssetPackage(ProgramAssetPackageDTO packageDTO);
}
