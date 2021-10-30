using HackSystem.Common;
using HackSystem.DataTransferObjects.MockServer;
using HackSystem.WebAPI.MockServer.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.TaskServer;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = CommonSense.Roles.CommanderRole)]
public class MockServerController : Controller
{
    private readonly ILogger<MockServerController> logger;
    private readonly IMapper mapper;
    private readonly IMockRouteRepository mockRouteRepository;

    public MockServerController(
        ILogger<MockServerController> logger,
        IMapper mapper,
        IMockRouteRepository mockRouteRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.mockRouteRepository = mockRouteRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<MockRouteResponse>> QueryMockRoutes()
    {
        this.logger.LogInformation($"Query mock routes...");
        var mocks = await this.mockRouteRepository.QueryMockRoutes();
        this.logger.LogInformation($"Found {mocks.Count()} Mock Routes.");
        var dtos = this.mapper.Map<IEnumerable<MockRouteResponse>>(mocks);
        return dtos;
    }
}
