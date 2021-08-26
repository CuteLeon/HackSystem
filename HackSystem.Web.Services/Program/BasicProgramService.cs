using System.Net.Http.Json;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Authentication;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Services.Program;

public class BasicProgramService : AuthenticatedServiceBase, IBasicProgramService
{
    public BasicProgramService(
        ILogger<BasicProgramService> logger,
        IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler,
        HttpClient httpClient)
        : base(logger, hackSystemAuthenticationStateHandler, httpClient)
    {
    }

    public async Task<IEnumerable<QueryUserBasicProgramMapDTO>> QueryUserBasicProgramMaps()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<QueryUserBasicProgramMapDTO>>("api/basicprogram/QueryUserBasicProgramMaps");
        return result;
    }

    public async Task<bool> SetUserBasicProgramHide(SetUserProgramHideDTO hideDTO)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramHide", hideDTO);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramPinToDock(SetUserBasicProgramPinToDockDTO pinToDockDTO)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramPinToDock", pinToDockDTO);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramPinToTop(SetUserBasicProgramPinToTopDTO pinToTopDTO)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramPinToTop", pinToTopDTO);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramRename(SetUserBasicProgramRenameDTO renameDTO)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramRename", renameDTO);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserBasicProgramMap(string programId)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.DeleteAsync($"api/basicprogram/DeleteUserBasicProgramMap?programId={programId}");
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}
