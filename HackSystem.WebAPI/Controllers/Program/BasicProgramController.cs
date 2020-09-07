using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HackSystem.Common;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers.Program
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = CommonSense.Roles.HackerRole)]
    public class BasicProgramController : ControllerBase
    {
        private readonly ILogger<BasicProgramController> logger;
        private readonly IMapper mapper;
        private readonly IBasicProgramDataService basicProgramDataService;

        public BasicProgramController(
            ILogger<BasicProgramController> logger,
            IMapper mapper,
            IBasicProgramDataService basicProgramDataService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.basicProgramDataService = basicProgramDataService;
        }

        [HttpGet]
        public async Task<IEnumerable<QueryBasicProgramDTO>> GetAll()
        {
            this.logger.LogInformation($"Query all Basic Programs.");
            var entities = await this.basicProgramDataService.ToArrayAsync();
            var result = this.mapper.Map<IEnumerable<QueryBasicProgramDTO>>(entities).ToArray();
            return result;
        }

        [HttpGet]
        public async Task<QueryBasicProgramDTO> Get(string programId)
        {
            this.logger.LogInformation($"Query Basic Programs id = {programId}");
            var entity = await this.basicProgramDataService.FindAsync(programId);
            var result = this.mapper.Map<QueryBasicProgramDTO>(entity);
            return result;
        }
    }
}
