using System.Net.Http.Json;
using HackSystem.Web.Application.Program;
using HackSystem.Web.Infrastructure.Authentication;
using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.Infrastructure.Program;

public class ProgramDetailService : AuthenticatedServiceBase, IProgramDetailService
{
    public ProgramDetailService(
        ILogger<ProgramDetailService> logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        HttpClient httpClient)
        : base(logger, authenticationTokenHandler, httpClient)
    {
    }

    public async Task<IEnumerable<UserProgramMapResponse>> QueryUserProgramMaps()
    {
        await this.AddAuthorizationHeaderAsync();
        var result = await this.httpClient.GetFromJsonAsync<IEnumerable<UserProgramMapResponse>>("api/ProgramDetail/QueryUserProgramMaps");
        return result;
    }

    public async Task<bool> SetUserProgramHide(UserProgramMapRequest hideRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramHide", hideRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramPinToDock(UserProgramMapRequest pinToDockRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramPinToDock", pinToDockRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramPinToTop(UserProgramMapRequest pinToTopRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramPinToTop", pinToTopRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramRename(UserProgramMapRequest renameRequest)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramRename", renameRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserProgramMap(string programId)
    {
        await this.AddAuthorizationHeaderAsync();
        var response = await this.httpClient.DeleteAsync($"api/ProgramDetail/DeleteUserProgramMap?programId={programId}");
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}
