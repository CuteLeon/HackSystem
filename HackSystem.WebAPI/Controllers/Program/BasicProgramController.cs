using System.Security.Authentication;
using HackSystem.Common;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.DataTransferObjects.Programs;
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
    private readonly IUserBasicProgramMapRepository userBasicProgramMapRepository;

    public BasicProgramController(
        ILogger<BasicProgramController> logger,
        IMapper mapper,
        UserManager<HackSystemUser> userManager,
        IUserBasicProgramMapRepository userBasicProgramMapRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.userManager = userManager;
        this.userBasicProgramMapRepository = userBasicProgramMapRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<UserBasicProgramMapResponse>> QueryUserBasicProgramMaps()
    {
        this.logger.LogInformation($"Query user basic program maps...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;

        var maps = await this.userBasicProgramMapRepository.QueryUserBasicProgramMaps(userId);
        this.logger.LogInformation($"Found {maps.Count()} basic program maps for user {user.UserName}.");
        var dtos = this.mapper.Map<IEnumerable<UserBasicProgramMapResponse>>(maps);
        return dtos;
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramHide(UserBasicProgramMapRequest hideRequest)
    {
        this.logger.LogInformation($"Hide basic program {hideRequest.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userBasicProgramMapRepository.SetUserBasicProgramPinToDesktop(userId, hideRequest.ProgramId, hideRequest.PinToDesktop.Value);
        this.logger.LogInformation($"Hide basic program {hideRequest.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramPinToDock(UserBasicProgramMapRequest pinToDockRequest)
    {
        this.logger.LogInformation($"Pin basic program {pinToDockRequest.ProgramId} to dock for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userBasicProgramMapRepository.SetUserBasicProgramPinToDock(userId, pinToDockRequest.ProgramId, pinToDockRequest.PinToDock.Value);
        this.logger.LogInformation($"Pin basic program {pinToDockRequest.ProgramId} to dock for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramPinToTop(UserBasicProgramMapRequest pinToTopRequest)
    {
        this.logger.LogInformation($"Pin basic program {pinToTopRequest.ProgramId} to top for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userBasicProgramMapRepository.SetUserBasicProgramPinToTop(userId, pinToTopRequest.ProgramId, pinToTopRequest.PinToTop.Value);
        this.logger.LogInformation($"Pin basic program {pinToTopRequest.ProgramId} to top for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserBasicProgramRename(UserBasicProgramMapRequest renameRequest)
    {
        this.logger.LogInformation($"Rename basic program {renameRequest.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userBasicProgramMapRepository.SetUserBasicProgramRename(userId, renameRequest.ProgramId, renameRequest.Rename);
        this.logger.LogInformation($"Rename basic program {renameRequest.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserBasicProgramMap(string programId)
    {
        this.logger.LogInformation($"Delete basic program {programId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userBasicProgramMapRepository.DeleteUserBasicProgramMap(userId, programId);
        this.logger.LogInformation($"Delete basic program {programId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }
}
