﻿using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowDestroyCommandHandler : IIntermediaryCommandHandler<WindowDestroyCommand>
{
    private readonly ILogger<ProcessDestroyCommandHandler> logger;
    private readonly IIntermediaryCommandSender commandSender;
    private readonly IIntermediaryRequestSender requestSender;

    public WindowDestroyCommandHandler(
        ILogger<ProcessDestroyCommandHandler> logger,
        IIntermediaryCommandSender commandSender,
        IIntermediaryRequestSender requestSender)
    {
        this.logger = logger;
        this.commandSender = commandSender;
        this.requestSender = requestSender;
    }

    public async Task<ValueTuple> Handle(WindowDestroyCommand request, CancellationToken cancellationToken)
    {
        var windowDetail = request.ProgramWindowDetail;
        var processDetail = request.ProgramWindowDetail.ProcessDetail;
        this.logger.LogInformation($"Handle Window destroy command: Window {windowDetail.WindowId} of Process {processDetail.ProcessId} ...");
        if (processDetail.RemoveWindowDetail(windowDetail))
        {
            _ = await this.requestSender.Send(new WindowScheduleRequest(request.ProgramWindowDetail, WindowScheduleStates.Destory));
        }
        else
        {
            this.logger.LogWarning($"Failed to close window {windowDetail.WindowId} from process {processDetail.ProcessId}...");
        }

        if (processDetail.ProgramDetail.ProgramEntryComponentType is not null &&
            !processDetail.GetWindowDetails().Any())
        {
            this.logger.LogInformation($"Send Destory process command of {processDetail.ProcessId} as all windows destoryed...");
            await this.commandSender.Send(new ProcessDestroyCommand(processDetail));
        }
        this.logger.LogInformation($"Window {request.ProgramWindowDetail.WindowId} destroy command handled.");
        return ValueTuple.Create();
    }
}