using HackSystem.LRU;
using HackSystem.Web.Component.Configurations;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
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
        this.logger.LogInformation($"Handle Window {request.ChangeStates} request {request.ProgramWindowDetail.Caption} ...");
        if (request.ChangeStates == WindowChangeStates.Launch)
        {
            request.ProgramWindowDetail.TierIndex = this.GetNewTierIndex();
            this.windowLRUContainer.Add(request.ProgramWindowDetail);
        }
        else if (request.ChangeStates == WindowChangeStates.Active)
        {
            if (this.windowLRUContainer.HeadValue == request.ProgramWindowDetail)
            {
                if (request.ProgramWindowDetail.WindowState == ProgramWindowStates.Minimized)
                    request.ProgramWindowDetail.WindowState = request.ProgramWindowDetail.LastWindowState;
                else
                    return new WindowScheduleResponse(request.ChangeStates, false);
            }
            else
            {
                if (request.ProgramWindowDetail.WindowState == ProgramWindowStates.Minimized)
                    request.ProgramWindowDetail.WindowState = request.ProgramWindowDetail.LastWindowState;
                request.ProgramWindowDetail.TierIndex = this.GetNewTierIndex();
                this.windowLRUContainer.BringToHead(request.ProgramWindowDetail);
            }
        }
        else if (request.ChangeStates == WindowChangeStates.Inactive)
        {
            var headWindow = this.windowLRUContainer.HeadValue!;
            if (headWindow == request.ProgramWindowDetail)
            {
                var previewWindow = this.windowLRUContainer.GetPreviousValue(headWindow);
                if (previewWindow is not null)
                    this.windowLRUContainer.BringToHead(previewWindow);
            }
            else
            {
                request.ProgramWindowDetail.TierIndex = headWindow.TierIndex;
                headWindow.TierIndex = this.GetNewTierIndex();
                this.windowLRUContainer.MoveToAfter(request.ProgramWindowDetail, this.windowLRUContainer.HeadValue!);
            }
        }
        else if (request.ChangeStates == WindowChangeStates.Destory)
        {
            request.ProgramWindowDetail.TierIndex = this.tierConfiguration.BasicProgramSubscript;
            this.windowLRUContainer.Remove(request.ProgramWindowDetail);
        }

        this.logger.LogInformation($"Window {request.ChangeStates} request handled, {request.ProgramWindowDetail.Caption} ({request.ProgramWindowDetail.TierIndex}).");
        this.OnWindowSchedule?.Invoke(request.ProgramWindowDetail);
        return new WindowScheduleResponse(request.ChangeStates, true);
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
