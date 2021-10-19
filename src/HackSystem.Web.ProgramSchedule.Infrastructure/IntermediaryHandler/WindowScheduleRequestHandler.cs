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
        if (request.ScheduleStates == WindowScheduleStates.Launch)
        {
            request.ProgramWindowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.Add(request.ProgramWindowDetail);
        }
        else if (request.ScheduleStates == WindowScheduleStates.Schedule)
        {
            if (this.windowLRUContainer.HeadValue == request.ProgramWindowDetail) return new WindowScheduleResponse(request.ScheduleStates, false);

            request.ProgramWindowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.BringToHead(request.ProgramWindowDetail);
        }
        else if (request.ScheduleStates == WindowScheduleStates.Destory)
        {
            request.ProgramWindowDetail.TierIndex = this.tierConfiguration.BasicProgramSubscript;
            this.windowLRUContainer.Remove(request.ProgramWindowDetail);
        }

        this.logger.LogInformation($"Window {request.ScheduleStates} request handled, {request.ProgramWindowDetail.Caption} ({request.ProgramWindowDetail.TierIndex}).");
        this.OnWindowSchedule?.Invoke(request.ProgramWindowDetail);
        return new WindowScheduleResponse(request.ScheduleStates, true);
    }

    private int GetNewTierIndex()
    {
        var newTierIndex = this.windowLRUContainer.HeadValue?.TierIndex + 1 ?? this.tierConfiguration.BasicProgramSubscript;
        if (newTierIndex >= this.tierConfiguration.BasicProgramSuperscript)
        {
            this.logger.LogInformation("Reach program superscript, resort all window tier index...");
            var tierIndex = this.tierConfiguration.BasicProgramSubscript;
            foreach (var window in this.windowLRUContainer.GetValuesFromTail())
            {
                window.TierIndex = tierIndex++;
            }
            newTierIndex = --tierIndex;
        }
        return newTierIndex;
    }
}
