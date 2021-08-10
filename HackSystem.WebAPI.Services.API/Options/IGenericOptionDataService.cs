using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Option;

namespace HackSystem.WebAPI.Services.Options
{
    public interface IGenericOptionDataService : IDataServiceBase<GenericOption>
    {
        Task<GenericOption> QueryGenericOptions(string optionName);

        Task<GenericOption> QueryGenericOptions(string optionName, string owner);

        Task<GenericOption> QueryGenericOptions(string optionName, string owner, string category);

        Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames);

        Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames, string owner);

        Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames, string owner, string category);
    }
}
