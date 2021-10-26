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
    public async Task<IActionResult> UpdateUserProgram(UserProgramMapRequest request)
    {
        this.logger.LogInformation($"Update use program map of {request.ProgramId} ...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;
        var map = await this.userProgramMapRepository.FindAsync(userId, request.ProgramId);

        if (request.PinToDock.HasValue) map.PinToDock = request.PinToDock.Value;

        await this.userProgramMapRepository.UpdateAsync(map);
        this.logger.LogInformation($"User program map of {request.ProgramId} for user {userId} updated.");
        return this.Ok();
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
