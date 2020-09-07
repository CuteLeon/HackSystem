using AutoMapper;
using HackSystem.Common;
using HackSystem.WebAPI.Services.API.DataServices.Program;
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
    }
}
