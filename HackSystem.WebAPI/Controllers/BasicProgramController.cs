using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BasicProgramController : ControllerBase
    {
        private readonly ILogger<BasicProgramController> logger;
        private readonly IBasicProgramDataService basicProgramDataService;

        public BasicProgramController(
            ILogger<BasicProgramController> logger,
            IBasicProgramDataService basicProgramDataService)
        {
            this.logger = logger;
            this.basicProgramDataService = basicProgramDataService;
        }

        [HttpGet]
        public async Task<IEnumerable<BasicProgram>> GetAll()
        {
            this.logger.LogInformation($"Query all Basic Programs.");
            return await this.basicProgramDataService.ToArrayAsync();
        }

        [HttpGet]
        public async Task<BasicProgram> Get(string programId)
        {
            this.logger.LogInformation($"Query Basic Programs id = {programId}");
            return await this.basicProgramDataService.FindAsync(programId);
        }

        [HttpPost]
        public async Task<BasicProgram> Post([FromBody] BasicProgram basicProgram)
        {
            this.logger.LogInformation($"Create Basic Programs: {basicProgram.Name}");
            var entity = await this.basicProgramDataService.AddAsync(basicProgram);
            return entity;
        }

        [HttpPut]
        public async Task<BasicProgram> Put([FromBody] BasicProgram basicProgram)
        {
            this.logger.LogInformation($"Update Basic Programs: {basicProgram.Name}");
            var entity = this.basicProgramDataService.Update(basicProgram);
            return entity;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string programId)
        {
            this.logger.LogInformation($"Delete Basic Programs id = {programId}");
            var entity = await this.basicProgramDataService.FindAsync(programId);
            this.basicProgramDataService.Remove(entity);
            return this.Ok();
        }
    }
}
