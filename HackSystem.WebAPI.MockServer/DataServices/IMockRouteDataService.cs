using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Mock;

namespace HackSystem.WebAPI.MockServers.DataServices
{
    public interface IMockRouteDataService : IDataServiceBase<MockRouteDetail>
    {
        Task<MockRouteDetail> QueryMockRoute(string uri, string method, string sourceHost);
    }
}
