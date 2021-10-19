using HackSystem.LRU;
using HackSystem.Web.Component.Configurations;
using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.IntermediaryHandler;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowScheduleRequestHandler : IWindowScheduleRequestHandler
{
    public event IWindowScheduleRequestHandler.WindowScheduleHandler? OnWindowSchedule;
    private readonly ILogger<WindowScheduleRequestHandler> logger;
    private readonly WebComponentTierConfiguration tierConfiguration;
    private readonly LRUContainer<string, ProgramWindowDetail> windowLRUContainer;

    public WindowScheduleRequestHandler(
        ILogger<WindowScheduleRequestHandler> logger,
        IOptionsMonitor<WebComponentTierConfiguration> tierConfiguration)
    {
        this.logger = logger;
        this.tierConfiguration = tierConfiguration.CurrentValue;
        this.windowLRUContainer = new(window => window.WindowId, int.MaxValue);
    }

    public async Task<WindowScheduleResponse> Handle(WindowScheduleRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Handle Window {request.ScheduleStates} request {request.ProgramWindowDetail.Caption} ...");
        var scheduled = true;
        switch (request.ScheduleStates)
        {
            case Abstractions.Enums.WindowScheduleStates.Schedule:
                {
                    scheduled = this.windowLRUContainer.BringToHead(request.ProgramWindowDetail);
                    break;
                }
            case Abstractions.Enums.WindowScheduleStates.Launch:
                {
                    this.windowLRUContainer.Add(request.ProgramWindowDetail);
                    break;
                }
            case Abstractions.Enums.WindowScheduleStates.Destory:
                {
                    this.windowLRUContainer.Remove(request.ProgramWindowDetail);
                    break;
                }
            default:
                break;
        }
        this.logger.LogInformation($"Window {request.ScheduleStates} request handled, {request.ProgramWindowDetail.Caption}.");

        if (scheduled && request.ScheduleStates != WindowScheduleStates.Destory)
        {
            // TODO: LEON: WHAT IF SET Z_-INDEX BY JS DIRECTLY IEnumerable<(WindowID, TierIndex)> ???!!!
            // TODO: LEON: Get index from options;
            // TODO: LEON: Just render z-index, and keep other datas;
            // TODO: LEON: Sort index from lowest to highest, set new index as Head's index + 1. And reset sort form lowest again if reach highest.
            request.ProgramWindowDetail.TierIndex = this.windowLRUContainer.HeadValue!.TierIndex + 1;
            this.OnWindowSchedule?.Invoke(request.ProgramWindowDetail);
        }
        return new WindowScheduleResponse(request.ScheduleStates, scheduled);
    }
}
