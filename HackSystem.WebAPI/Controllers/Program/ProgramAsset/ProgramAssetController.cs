using System.Security.Authentication;
using AutoMapper;
using HackSystem.Common;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebDataTransfer.Program.ProgramAsset;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IUserBasicProgramMapDataService userBasicProgramMapDataService;
    private readonly IProgramAssetService programAssetService;

    public ProgramAssetController(
        ILogger<ProgramAssetController> logger,
        UserManager<HackSystemUser> userManager,
        IMapper mapper,
        IUserBasicProgramMapDataService userBasicProgramMapDataService,
        IProgramAssetService programAssetService)
    {
        this.logger = logger;
        this.userManager = userManager;
        this.mapper = mapper;
        this.userBasicProgramMapDataService = userBasicProgramMapDataService;
        this.programAssetService = programAssetService;
    }

    [HttpGet]
    public async Task<IActionResult> QueryProgramAssetList(string programId)
    {
        if (!await CheckUserBasicProgramMap(programId))
        {
            return this.Forbid();
        }

        var package = await this.programAssetService.QueryProgramAssetList(programId);
        this.logger.LogInformation($"Found {package.ProgramAssets.Count()} program assets of program {programId}.");
        var packageDTO = this.mapper.Map<ProgramAssetPackageDTO>(package);
        return this.Ok(packageDTO);
    }

    [HttpGet]
    public async Task<IActionResult> QueryProgramAssetPackage(string programId)
    {
        if (!await CheckUserBasicProgramMap(programId))
        {
            return this.Forbid();
        }

        var package = await this.programAssetService.QueryProgramAssetPackage(programId);
        this.logger.LogInformation($"Found {package.ProgramAssets.Count()} program assets of program {programId}.");
        var packageDTO = this.mapper.Map<ProgramAssetPackageDTO>(package);
        return this.Ok(packageDTO);
    }

    [HttpPost]
    public async Task<IActionResult> QueryProgramAssetPackage(ProgramAssetPackageDTO packageDTO)
    {
        if (!await CheckUserBasicProgramMap(packageDTO.ProgramId))
        {
            return this.Forbid();
        }

        var package = this.mapper.Map<ProgramAssetPackage>(packageDTO);
        package = await this.programAssetService.QueryProgramAssetPackage(package);
        this.logger.LogInformation($"Found {packageDTO.ProgramAssets.Count()} program assets of program {packageDTO.ProgramId}.");
        packageDTO = this.mapper.Map<ProgramAssetPackageDTO>(package);
        return this.Ok(packageDTO);
    }

    private async Task<bool> CheckUserBasicProgramMap(string programId)
    {
        if (string.IsNullOrWhiteSpace(programId))
        {
            throw new ArgumentException($"'{nameof(programId)}' cannot be null or whitespace.", nameof(programId));
        }

        this.logger.LogInformation($"Query program asset package...");
        var userName = this.HttpContext.User?.Identity?.Name ?? throw new AuthenticationException();
        var user = await this.userManager.FindByNameAsync(userName) ?? throw new AuthenticationException();
        var userId = user.Id;

        var hasAccess = await this.userBasicProgramMapDataService.CheckUserBasicProgramMap(userId, programId);
        if (!hasAccess)
        {
            this.logger.LogWarning($"User {userName} has no access to program asset of program {programId}.");
        }
        return hasAccess;
    }
}
