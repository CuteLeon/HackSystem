using System.Security.Authentication;
using HackSystem.Common;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebDataTransfer.Program;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.Program;

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
        this.basicProgramDataService = basicProgramDataService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserBasicProgramMapDTO>> QueryUserBasicProgramMaps()
    {
        this.logger.LogInformation($"Query user basic program maps...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;

        var maps = await this.basicProgramDataService.QueryUserBasicProgramMaps(userId);
        this.logger.LogInformation($"Found {maps.Count()} basic program maps for user {user.UserName}.");
        var dtos = this.mapper.Map<IEnumerable<UserBasicProgramMapDTO>>(maps);
        return dtos;
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramHide(SetUserProgramHideDTO hideDTO)
    {
        this.logger.LogInformation($"Hide basic program {hideDTO.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.basicProgramDataService.SetUserBasicProgramHide(userId, hideDTO.ProgramId, hideDTO.Hide);
        this.logger.LogInformation($"Hide basic program {hideDTO.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramPinToDock(SetUserBasicProgramPinToDockDTO pinToDockDTO)
    {
        this.logger.LogInformation($"Pin basic program {pinToDockDTO.ProgramId} to dock for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.basicProgramDataService.SetUserBasicProgramPinToDock(userId, pinToDockDTO.ProgramId, pinToDockDTO.PinToDock);
        this.logger.LogInformation($"Pin basic program {pinToDockDTO.ProgramId} to dock for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramPinToTop(SetUserBasicProgramPinToTopDTO pinToTopDTO)
    {
        this.logger.LogInformation($"Pin basic program {pinToTopDTO.ProgramId} to top for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.basicProgramDataService.SetUserBasicProgramPinToTop(userId, pinToTopDTO.ProgramId, pinToTopDTO.PinToTop);
        this.logger.LogInformation($"Pin basic program {pinToTopDTO.ProgramId} to top for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramRename(SetUserBasicProgramRenameDTO renameDTO)
    {
        this.logger.LogInformation($"Rename basic program {renameDTO.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.basicProgramDataService.SetUserBasicProgramRename(userId, renameDTO.ProgramId, renameDTO.Rename);
        this.logger.LogInformation($"Rename basic program {renameDTO.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserBasicProgramMap(string programId)
    {
        this.logger.LogInformation($"Delete basic program {programId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.basicProgramDataService.DeleteUserBasicProgramMap(userId, programId);
        this.logger.LogInformation($"Delete basic program {programId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }
}
