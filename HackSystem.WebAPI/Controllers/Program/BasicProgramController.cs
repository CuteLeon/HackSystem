using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers.Program
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
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
