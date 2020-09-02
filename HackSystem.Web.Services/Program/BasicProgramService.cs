using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Extensions;
using HackSystem.WebDataTransfer.Program;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HackSystem.Web.Services.Program
{
    public class BasicProgramService : IBasicProgramService
    {
        private readonly ILogger<BasicProgramService> logger;
        private readonly IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler;
        private readonly HttpClient httpClient;

        public BasicProgramService(
            ILogger<BasicProgramService> logger,
            IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler,
            HttpClient httpClient)
        {
            this.logger = logger;
            this.hackSystemAuthenticationStateHandler = hackSystemAuthenticationStateHandler;
            this.httpClient = httpClient;
        }

        public async Task<QueryBasicProgramDTO> Create(CreateBasicProgramDTO basicProgram)
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
            var response = await this.httpClient.PostAsJsonAsync("api/basicprogram/create", basicProgram);
            var result = JsonConvert.DeserializeObject<QueryBasicProgramDTO>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<QueryBasicProgramDTO> Delete(string programId)
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
            var response = await this.httpClient.DeleteAsync($"api/basicprogram/delete?programId={programId}");
            var result = JsonConvert.DeserializeObject<QueryBasicProgramDTO>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<QueryBasicProgramDTO> Get(string programId)
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
            var basicProgram = await httpClient.GetFromJsonAsync<QueryBasicProgramDTO>($"api/basicprogram/get?programid={programId}");
            return basicProgram;
        }

        public async Task<IEnumerable<QueryBasicProgramDTO>> GetAll()
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
            var basicPrograms = await httpClient.GetFromJsonAsync<IEnumerable<QueryBasicProgramDTO>>("api/basicprogram/getall");
            return basicPrograms;
        }

        public async Task<QueryBasicProgramDTO> Update(UpdateBasicProgramDTO basicProgram)
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
            var response = await httpClient.PutAsJsonAsync($"api/basicprogram/update", basicProgram);
            var result = JsonConvert.DeserializeObject<QueryBasicProgramDTO>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
