using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.Domain.Entity;

namespace HackSystem.WebAPI.Application.Repository;

public interface IGenericOptionRepository : IRepositoryBase<GenericOption>
{
    Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null);
}
