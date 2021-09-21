using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.Model.Option;

namespace HackSystem.WebAPI.Services.Options;

public interface IGenericOptionDataService : IRepositoryBase<GenericOption>
{
    Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null);
}
