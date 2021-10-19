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
public class ProgramDetailController : ControllerBase
{
    private readonly ILogger<ProgramDetailController> logger;
    private readonly IMapper mapper;
    private readonly UserManager<HackSystemUser> userManager;
    private readonly IUserProgramMapRepository userProgramMapRepository;

    public ProgramDetailController(
        ILogger<ProgramDetailController> logger,
        IMapper mapper,
        UserManager<HackSystemUser> userManager,
        IUserProgramMapRepository userProgramMapRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.userManager = userManager;
        this.userProgramMapRepository = userProgramMapRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<UserProgramMapResponse>> QueryUserProgramMaps()
    {
        this.logger.LogInformation($"Query user program maps...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;

        var maps = await this.userProgramMapRepository.QueryUserProgramMaps(userId);
        this.logger.LogInformation($"Found {maps.Count()} program maps for user {user.UserName}.");
        var dtos = this.mapper.Map<IEnumerable<UserProgramMapResponse>>(maps);
        return dtos;
    }

    [HttpPut]
    public async Task<IActionResult> SetUserProgramHide(UserProgramMapRequest hideRequest)
    {
        this.logger.LogInformation($"Hide program {hideRequest.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userProgramMapRepository.SetUserProgramPinToDesktop(userId, hideRequest.ProgramId, hideRequest.PinToDesktop.Value);
        this.logger.LogInformation($"Hide program {hideRequest.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserProgramPinToDock(UserProgramMapRequest pinToDockRequest)
    {
        this.logger.LogInformation($"Pin program {pinToDockRequest.ProgramId} to dock for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userProgramMapRepository.SetUserProgramPinToDock(userId, pinToDockRequest.ProgramId, pinToDockRequest.PinToDock.Value);
        this.logger.LogInformation($"Pin program {pinToDockRequest.ProgramId} to dock for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserProgramPinToTop(UserProgramMapRequest pinToTopRequest)
    {
        this.logger.LogInformation($"Pin program {pinToTopRequest.ProgramId} to top for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userProgramMapRepository.SetUserProgramPinToTop(userId, pinToTopRequest.ProgramId, pinToTopRequest.PinToTop.Value);
        this.logger.LogInformation($"Pin program {pinToTopRequest.ProgramId} to top for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> SetUserProgramRename(UserProgramMapRequest renameRequest)
    {
        this.logger.LogInformation($"Rename program {renameRequest.ProgramId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userProgramMapRepository.SetUserProgramRename(userId, renameRequest.ProgramId, renameRequest.Rename);
        this.logger.LogInformation($"Rename program {renameRequest.ProgramId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserProgramMap(string programId)
    {
        this.logger.LogInformation($"Delete program {programId} for user...");
        var userId = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var result = await this.userProgramMapRepository.DeleteUserProgramMap(userId, programId);
        this.logger.LogInformation($"Delete program {programId} for user {userId} {(result ? "successfully" : "failed")}.");
        return result ? this.Ok(result) : this.BadRequest(result);
    }
}
