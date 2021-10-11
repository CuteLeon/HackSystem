using System.Net.Http.Json;
using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Application.Program;
using HackSystem.Web.Authentication.WebServices;

namespace HackSystem.Web.Infrastructure.Program;

public class ProgramDetailService : AuthenticatedServiceBase, IProgramDetailService
{
    public ProgramDetailService(
        ILogger<ProgramDetailService> logger,
        IHttpClientFactory httpClientFactory)
        : base(logger, httpClientFactory)
    {
    }

    public async Task<IEnumerable<UserProgramMapResponse>> QueryUserProgramMaps()
    {
        var result = await this.HttpClient.GetFromJsonAsync<IEnumerable<UserProgramMapResponse>>("api/ProgramDetail/QueryUserProgramMaps");
        return result;
    }

    public async Task<bool> SetUserProgramHide(UserProgramMapRequest hideRequest)
    {
        var response = await this.HttpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramHide", hideRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramPinToDock(UserProgramMapRequest pinToDockRequest)
    {
        var response = await this.HttpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramPinToDock", pinToDockRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramPinToTop(UserProgramMapRequest pinToTopRequest)
    {
        var response = await this.HttpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramPinToTop", pinToTopRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetUserProgramRename(UserProgramMapRequest renameRequest)
    {
        var response = await this.HttpClient.PutAsJsonAsync("api/ProgramDetail/SetUserProgramRename", renameRequest);
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteUserProgramMap(string programId)
    {
        var response = await this.HttpClient.DeleteAsync($"api/ProgramDetail/DeleteUserProgramMap?programId={programId}");
        response.EnsureSuccessStatusCode();
        return response.IsSuccessStatusCode;
    }
}
