using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Option;

namespace HackSystem.WebAPI.Services.Options
{
    public interface IGenericOptionDataService : IDataServiceBase<GenericOption>
    {
        Task<IEnumerable<GenericOption>> QueryGenericOptionsByName(string optionName);

        Task<IEnumerable<GenericOption>> QueryGenericOptionsByOwnerAndName(string owner, string optionName);

        Task<GenericOption> QueryGenericOptionsByOwnerAndCategoryAndName(string owner, string category, string optionName);
    }
}
