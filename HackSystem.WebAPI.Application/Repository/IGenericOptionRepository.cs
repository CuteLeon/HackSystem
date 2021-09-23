using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.Domain.Entity;

namespace HackSystem.WebAPI.Application.Repository;

public interface IGenericOptionRepository : IRepositoryBase<GenericOption>
{
    Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null);
}
