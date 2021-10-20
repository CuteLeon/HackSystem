using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;

public class WindowDestroyCommandHandler : IIntermediaryCommandHandler<WindowDestroyCommand>
{
    private readonly ILogger<WindowDestroyCommandHandler> logger;
    private readonly IWindowDestroyer windowDestroyer;

    public WindowDestroyCommandHandler(
        ILogger<WindowDestroyCommandHandler> logger,
        IWindowDestroyer windowDestroyer)
    {
        this.logger = logger;
        this.windowDestroyer = windowDestroyer;
    }

    public async Task<ValueTuple> Handle(WindowDestroyCommand request, CancellationToken cancellationToken)
    {
        var windowDetail = request.ProgramWindowDetail;
        this.logger.LogInformation($"Handle Window destroy command {windowDetail.Caption} ({windowDetail.WindowId}) ...");
        await this.windowDestroyer.DestroyWindow(windowDetail);
        this.logger.LogInformation($"Window destroy command {windowDetail.Caption} ({windowDetail.WindowId}) handled.");
        return ValueTuple.Create();
    }
}
