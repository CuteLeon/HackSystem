using System.Net.Http.Json;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Program.ProgramAsset;
using HackSystem.Web.Services.Authentication;
using HackSystem.WebDataTransfer.Program.ProgramAsset;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HackSystem.Web.Services.Program.ProgramAsset;

public class ProgramAssetService : AuthenticatedServiceBase, IProgramAssetService
{
    public ProgramAssetService(
        ILogger<ProgramAssetService> logger,
        IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler,
        HttpClient httpClient)
        : base(logger, hackSystemAuthenticationStateHandler, httpClient)
    {
    }

    public async Task<ProgramAssetPackageDTO> QueryProgramAssetList(string programId)
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<ProgramAssetPackageDTO>($"api/programasset/QueryProgramAssetList?programId={programId}");
        return result;
    }

    public async Task<ProgramAssetPackageDTO> QueryProgramAssetPackage(ProgramAssetPackageDTO packageDTO)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PostAsJsonAsync("api/programasset/QueryProgramAssetPackage", packageDTO);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Invalid operation exception ({response.StatusCode}) when requesting program asset package of program {packageDTO.ProgramId}.");
        }

        packageDTO = JsonConvert.DeserializeObject<ProgramAssetPackageDTO>(await response.Content.ReadAsStringAsync());
        return packageDTO;
    }
}
