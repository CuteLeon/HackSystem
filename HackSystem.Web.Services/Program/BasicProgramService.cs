using System.Net.Http.Json;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Authentication;
using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.Services.Program;

public class BasicProgramService : AuthenticatedServiceBase, IBasicProgramService
{
    public BasicProgramService(
        ILogger<BasicProgramService> logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        HttpClient httpClient)
        : base(logger, authenticationTokenHandler, httpClient)
    {
    }

    public async Task<IEnumerable<UserBasicProgramMapResponse>> QueryUserBasicProgramMaps()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<UserBasicProgramMapResponse>>("api/basicprogram/QueryUserBasicProgramMaps");
        return result;
    }

    public async Task<bool> SetUserBasicProgramHide(UserBasicProgramMapRequest hideRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramHide", hideRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramPinToDock(UserBasicProgramMapRequest pinToDockRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramPinToDock", pinToDockRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramPinToTop(UserBasicProgramMapRequest pinToTopRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramPinToTop", pinToTopRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserBasicProgramRename(UserBasicProgramMapRequest renameRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/basicprogram/SetUserBasicProgramRename", renameRequest);
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
