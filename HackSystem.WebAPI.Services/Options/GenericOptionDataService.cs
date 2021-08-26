using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Options;

    public class GenericOptionDataService : DataServiceBase<GenericOption>, IGenericOptionDataService
    {
        public GenericOptionDataService(
            ILogger<GenericOptionDataService> logger,
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
