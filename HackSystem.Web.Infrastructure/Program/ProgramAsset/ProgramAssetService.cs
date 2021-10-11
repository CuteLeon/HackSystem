using System.Net.Http.Json;
using HackSystem.DataTransferObjects.Programs.ProgramAssets;
using HackSystem.Web.Application.Program.ProgramAsset;
using HackSystem.Web.Authentication.WebServices;
using Newtonsoft.Json;

namespace HackSystem.Web.Infrastructure.Program.ProgramAsset;

public class ProgramAssetService : AuthenticatedServiceBase, IProgramAssetService
{
    public ProgramAssetService(
        ILogger<ProgramAssetService> logger,
        IHttpClientFactory httpClientFactory)
        : base(logger, httpClientFactory)
    {
    }

    public async Task<ProgramAssetPackageResponse> QueryProgramAssetList(string programId)
    {
        var result = await this.HttpClient.GetFromJsonAsync<ProgramAssetPackageResponse>($"api/programasset/QueryProgramAssetList?programId={programId}");
        return result;
    }

    public async Task<ProgramAssetPackageResponse> QueryProgramAssetPackage(ProgramAssetPackageRequest packageRequest)
    {
        var response = await this.HttpClient.PostAsJsonAsync("api/programasset/QueryProgramAssetPackage", packageRequest);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Invalid operation exception ({response.StatusCode}) when requesting program asset package of program {packageRequest.ProgramId}.");
        }

        var packageResult = JsonConvert.DeserializeObject<ProgramAssetPackageResponse>(await response.Content.ReadAsStringAsync());
        return packageResult;
    }
}
