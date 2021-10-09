using HackSystem.Intermediary.Application;

namespace HackSystem.Web.Component.ToastContainer;

public class ToastHandler : IToastHandler
{
    private readonly ILogger<ToastHandler> logger;
    private readonly IIntermediaryEventPublisher eventPublisher;

    public ToastHandler(
        ILogger<ToastHandler> logger,
        IIntermediaryEventPublisher eventPublisher)
    {
        this.logger = logger;
        this.eventPublisher = eventPublisher;
    }

    public async Task PopupToast(ToastDetail toastDetail)
    {
        this.logger.LogInformation($"Popup toast: {toastDetail.Title}...");
        await this.eventPublisher.Publish(new ToastEvent { ToastDetail = toastDetail });
    }
}
