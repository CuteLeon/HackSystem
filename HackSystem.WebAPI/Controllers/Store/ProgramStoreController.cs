using System.Threading.Tasks;
using AutoMapper;
using HackSystem.Common;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers.Store
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = CommonSense.Roles.CommanderRole)]
    public class ProgramStoreController : Controller
    {
        private readonly ILogger<ProgramStoreController> logger;
        private readonly IMapper mapper;
        private readonly IBasicProgramDataService basicProgramDataService;

        public ProgramStoreController(
            ILogger<ProgramStoreController> logger,
            IMapper mapper,
            IBasicProgramDataService basicProgramDataService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.basicProgramDataService = basicProgramDataService;
        }

        [HttpPost]
        public async Task<QueryBasicProgramDTO> Create([FromBody] CreateBasicProgramDTO basicProgram)
        {
            this.logger.LogInformation($"Create Basic Programs: {basicProgram.Name}");
            var entity = this.mapper.Map<BasicProgram>(basicProgram);
            entity = await this.basicProgramDataService.AddAsync(entity);
            var result = this.mapper.Map<QueryBasicProgramDTO>(entity);
            return result;
        }

        [HttpPut]
        public async Task<QueryBasicProgramDTO> Update([FromBody] UpdateBasicProgramDTO basicProgram)
        {
            this.logger.LogInformation($"Update Basic Programs: {basicProgram.Name}");
            var entity = this.mapper.Map<BasicProgram>(basicProgram);
            entity = this.basicProgramDataService.Update(entity);
            var result = this.mapper.Map<QueryBasicProgramDTO>(entity);
            return result;
        }

        [HttpDelete]
        public async Task<QueryBasicProgramDTO> Delete(string programId)
        {
            this.logger.LogInformation($"Delete Basic Programs id = {programId}");
            var entity = await this.basicProgramDataService.FindAsync(programId);
            this.basicProgramDataService.Remove(entity);
            var result = this.mapper.Map<QueryBasicProgramDTO>(entity);
            return result;
        }
    }
}
