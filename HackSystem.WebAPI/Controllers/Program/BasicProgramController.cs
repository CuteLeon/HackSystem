using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using HackSystem.Common;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<HackSystemUser> userManager;
        private readonly IUserBasicProgramMapDataService basicProgramDataService;

        public BasicProgramController(
            ILogger<BasicProgramController> logger,
            IMapper mapper,
            UserManager<HackSystemUser> userManager,
            IUserBasicProgramMapDataService basicProgramDataService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userManager = userManager;
            this.basicProgramDataService = basicProgramDataService;
        }

        [HttpGet]
        public async Task<IEnumerable<QueryUserBasicProgramMapDTO>> QueryUserBasicProgramMaps()
        {
            var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
            var userId = user.Id;
            var maps = await this.basicProgramDataService.QueryUserBasicProgramMaps(userId);
            var dtos = this.mapper.Map<IEnumerable<QueryUserBasicProgramMapDTO>>(maps);
            return dtos;
        }

        [HttpPut]
        public async Task<IActionResult> SetUserBasicProgramHide(SetUserProgramHideDTO hideDTO)
        {
            var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var result = await this.basicProgramDataService.SetUserBasicProgramHide(userId, hideDTO.ProgramId, hideDTO.Hide);
            return result ? this.Ok(result) : this.BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> SetUserBasicProgramPinToDock(SetUserBasicProgramPinToDockDTO pinToDockDTO)
        {
            var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var result = await this.basicProgramDataService.SetUserBasicProgramPinToDock(userId, pinToDockDTO.ProgramId, pinToDockDTO.PinToDock);
            return result ? this.Ok(result) : this.BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> SetUserBasicProgramPinToTop(SetUserBasicProgramPinToTopDTO pinToTopDTO)
        {
            var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var result = await this.basicProgramDataService.SetUserBasicProgramPinToTop(userId, pinToTopDTO.ProgramId, pinToTopDTO.PinToTop);
            return result ? this.Ok(result) : this.BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> SetUserBasicProgramRename(SetUserBasicProgramRenameDTO renameDTO)
        {
            var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var result = await this.basicProgramDataService.SetUserBasicProgramRename(userId, renameDTO.ProgramId, renameDTO.Rename);
            return result ? this.Ok(result) : this.BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserBasicProgramMap(string programId)
        {
            var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
            var result = await this.basicProgramDataService.DeleteUserBasicProgramMap(userId, programId);
            return result ? this.Ok(result) : this.BadRequest(result);
        }
    }
}
