using HackSystem.Intermediary.Application;

namespace HackSystem.Web.Component.ToastContainer;

public class ToastHandler : IToastHandler
{
    private readonly ILogger<ToastHandler> logger;
    private readonly IIntermediaryPublisher publisher;

    public ToastHandler(
        ILogger<ToastHandler> logger,
        IIntermediaryPublisher publisher)
    {
        this.logger = logger;
        this.publisher = publisher;
    }

    public async Task PopupToast(ToastDetail toastDetail)
    {
        this.logger.LogInformation($"Popup toast: {toastDetail.Title}...");
        await this.publisher.PublishEvent(new ToastEvent { ToastDetail = toastDetail });
    }
}
