using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Options
{
    public class GenericOptionDataService : DataServiceBase<GenericOption>, IGenericOptionDataService
    {
        public GenericOptionDataService(
            ILogger<GenericOptionDataService> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<GenericOption> QueryGenericOptions(string optionName)
        => await this.AsQueryable()
            .FirstOrDefaultAsync(o => o.OptionName == optionName && string.IsNullOrEmpty(o.Category) && string.IsNullOrEmpty(o.OwnerLevel));

        public async Task<GenericOption> QueryGenericOptions(string optionName, string owner)
        => await this.AsQueryable()
            .FirstOrDefaultAsync(o => o.OptionName == optionName && o.OwnerLevel == owner && string.IsNullOrEmpty(o.Category));

        public async Task<GenericOption> QueryGenericOptions(string optionName, string owner, string category)
        => await this.AsQueryable()
            .FirstOrDefaultAsync(o => o.OptionName == optionName && o.OwnerLevel == owner && o.Category == category);

        public async Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames)
        => this.AsQueryable()
            .Where(o => optionNames.Contains(o.OptionName) && string.IsNullOrEmpty(o.Category) && string.IsNullOrEmpty(o.OwnerLevel));

        public async Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames, string owner)
        => this.AsQueryable()
            .Where(o => optionNames.Contains(o.OptionName) && o.OwnerLevel == owner && string.IsNullOrEmpty(o.Category));

        public async Task<IEnumerable<GenericOption>> QueryGenericOptions(IEnumerable<string> optionNames, string owner, string category)
        => this.AsQueryable()
            .Where(o => optionNames.Contains(o.OptionName) && o.OwnerLevel == owner && o.Category == category);
    }
}
