using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Scheduler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowScheduleRequestHandler : IWindowScheduleRequestHandler
{
    private readonly ILogger<WindowScheduleRequestHandler> logger;
    private readonly IWindowScheduler windowScheduler;

    public WindowScheduleRequestHandler(
        ILogger<WindowScheduleRequestHandler> logger,
        IWindowScheduler windowScheduler)
    {
        this.logger = logger;
        this.windowScheduler = windowScheduler;
    }

    public async Task<WindowScheduleResponse> Handle(WindowScheduleRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Window {request.ChangeState} request {request.ProgramWindowDetail.Caption} ...");
        var scheduled = await this.windowScheduler.Schedule(request.ProgramWindowDetail, request.ChangeState);
        this.logger.LogInformation($"Window {request.ChangeState} request {request.ProgramWindowDetail.Caption} handled.");
        return new WindowScheduleResponse(request.ChangeState, scheduled);
    }
}
