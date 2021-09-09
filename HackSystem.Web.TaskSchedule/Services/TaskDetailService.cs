using System.Net.Http.Json;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.Authentication;
using HackSystem.WebDataTransfer.TaskServer;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.TaskSchedule.Services
{
    public class TaskDetailService : AuthenticatedServiceBase, ITaskDetailService
    {
        public TaskDetailService(
            ILogger<TaskDetailService> logger,
            IHackSystemAuthenticationStateProvider hackSystemAuthenticationStateProvider,
            HttpClient httpClient)
            : base(logger, hackSystemAuthenticationStateProvider, httpClient)
        {
        }

        public async Task<IEnumerable<TaskDetailDTO>> QueryTasks()
        {
            await this.AddAuthorizationHeaderAsync();
            var result = await this.httpClient.GetFromJsonAsync<IEnumerable<TaskDetailDTO>>("api/taskserver/QueryTasks");
            return result;
        }
    }
}
