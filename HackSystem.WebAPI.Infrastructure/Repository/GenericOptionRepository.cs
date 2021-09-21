using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.Domain.Entity;

namespace HackSystem.WebAPI.Infrastructure.Repository;

public class GenericOptionRepository : RepositoryBase<GenericOption>, IGenericOptionRepository
{
    public GenericOptionRepository(
        ILogger<GenericOptionRepository> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }

    public async Task<GenericOption> QueryGenericOption(string optionName, string owner = null, string category = null)
        => await this
            .AsQueryable()
            .Where(
                o => (o.OptionName == optionName) &&
                (string.IsNullOrEmpty(o.OwnerLevel) || o.OwnerLevel == owner) &&
                (string.IsNullOrEmpty(o.Category) || o.Category == category))
            .OrderByDescending(o => o.OwnerLevel)
            .ThenByDescending(o => o.Category)
            .FirstOrDefaultAsync();
}
