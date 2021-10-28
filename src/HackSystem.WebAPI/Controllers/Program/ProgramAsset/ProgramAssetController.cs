using System.Security.Authentication;
using HackSystem.Common;
using HackSystem.WebAPI.Domain.Attributes;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Application.Repository.ProgramAssets;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;
using HackSystem.DataTransferObjects.Programs.ProgramAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.Program.ProgramAsset;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = CommonSense.Roles.HackerRole)]
public class ProgramAssetController : ControllerBase
{
    private readonly ILogger<ProgramAssetController> logger;
    private readonly UserManager<HackSystemUser> userManager;
    private readonly IMapper mapper;
    private readonly IUserProgramMapRepository userProgramMapRepository;
    private readonly IProgramAssetService programAssetService;

    public ProgramAssetController(
        ILogger<ProgramAssetController> logger,
        UserManager<HackSystemUser> userManager,
        IMapper mapper,
        IUserProgramMapRepository userProgramMapRepository,
        IProgramAssetService programAssetService)
    {
        this.logger = logger;
        this.userManager = userManager;
        this.mapper = mapper;
        this.userProgramMapRepository = userProgramMapRepository;
        this.programAssetService = programAssetService;
    }

    [HttpGet]
    public async Task<IActionResult> QueryProgramAssetList(string programId)
    {
        if (!await CheckUserProgramMap(programId))
        {
            return this.Forbid();
        }

        ProgramAssetPackage package = default;
        try
        {
            package = await this.programAssetService.QueryProgramAssetList(programId);
        }
        catch (DirectoryNotFoundException)
        {
            return this.NotFound();
        }
        this.logger.LogInformation($"Found {package.ProgramAssets.Count()} program assets of program {programId}.");
        var packageRequest = this.mapper.Map<ProgramAssetPackageResponse>(package);
        return this.Ok(packageRequest);
    }

    [HttpGet]
    [AllowAnonymous]
    [LogActionFilter(noLogResponseBody: true)]
    public async Task<IActionResult> QueryProgramIcon(string programId)
    {
        var iconBytes = await this.programAssetService.QueryProgramIcon(programId);
        this.logger.LogInformation($"Found icon of program {programId}.");
        return this.File(iconBytes, "image/png");
    }

    [HttpGet]
    [LogActionFilter(noLogResponseBody: true)]
    public async Task<IActionResult> QueryProgramAssetPackage(string programId)
    {
        if (!await CheckUserProgramMap(programId))
        {
            return this.Forbid();
        }

        var package = await this.programAssetService.QueryProgramAssetPackage(programId);
        this.logger.LogInformation($"Found {package.ProgramAssets.Count()} program assets of program {programId}.");
        var packageRequest = this.mapper.Map<ProgramAssetPackageResponse>(package);
        return this.Ok(packageRequest);
    }

    [HttpPost]
    [LogActionFilter(noLogResponseBody: true)]
    public async Task<IActionResult> QueryProgramAssetPackage(ProgramAssetPackageRequest packageRequest)
    {
        if (!await CheckUserProgramMap(packageRequest.ProgramId))
        {
            return this.Forbid();
        }

        var package = this.mapper.Map<ProgramAssetPackage>(packageRequest);
        package = await this.programAssetService.QueryProgramAssetPackage(package);
        this.logger.LogInformation($"Found {packageRequest.ProgramAssets.Count()} program assets of program {packageRequest.ProgramId}.");
        var packageResult = this.mapper.Map<ProgramAssetPackageResponse>(package);
        return this.Ok(packageResult);
    }

    private async Task<bool> CheckUserProgramMap(string programId)
    {
        if (string.IsNullOrWhiteSpace(programId))
        {
            throw new ArgumentException($"'{nameof(programId)}' cannot be null or whitespace.", nameof(programId));
        }

        this.logger.LogInformation($"Query program asset package...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;

        var hasAccess = await this.userProgramMapRepository.CheckUserProgramMap(userId, programId);
        if (!hasAccess)
        {
            this.logger.LogWarning($"User {userName} has no access to program asset of program {programId}.");
        }
        return hasAccess;
    }
}
