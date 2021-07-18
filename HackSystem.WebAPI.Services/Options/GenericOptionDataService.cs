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

        public async Task<IEnumerable<GenericOption>> QueryGenericOptionsByName(string optionName)
            => this.AsQueryable().Where(o => o.OptionName == optionName);

        public async Task<GenericOption> QueryGenericOptionsByOwnerAndCategoryAndName(string owner, string category, string optionName)
            => await this.AsQueryable().FirstOrDefaultAsync(o => o.OptionName == optionName && o.OwnerLevel == owner && o.Category == category);

        public async Task<IEnumerable<GenericOption>> QueryGenericOptionsByOwnerAndName(string owner, string optionName)
            => this.AsQueryable().Where(o => o.OptionName == optionName && o.OwnerLevel == owner);
    }
}
