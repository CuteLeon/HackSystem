using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Option;

namespace HackSystem.WebAPI.Services.Options;

    public interface IGenericOptionDataService : IDataServiceBase<GenericOption>
    {
        Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null);
    }
