using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.IntermediaryHandler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowScheduleRequestHandler : IWindowScheduleRequestHandler
{
    public event IWindowScheduleRequestHandler.WindowScheduleHandler? OnWindowSchedule;
    private readonly ILogger<WindowScheduleRequestHandler> logger;

    public WindowScheduleRequestHandler(
        ILogger<WindowScheduleRequestHandler> logger)
    {
        this.logger = logger;
    }

    public async Task<WindowScheduleResponse> Handle(WindowScheduleRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Window {request.ScheduleStates} request {request.ProgramWindowDetail.Caption} ...");
        // TODO: Leon: LRU link
        this.logger.LogInformation($"Window {request.ScheduleStates} request handled, {request.ProgramWindowDetail.Caption}.");
        this.OnWindowSchedule?.Invoke(request.ProgramWindowDetail);
        return new WindowScheduleResponse(request.ScheduleStates, true);
    }
}
