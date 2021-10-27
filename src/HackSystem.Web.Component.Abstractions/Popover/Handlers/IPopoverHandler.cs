namespace HackSystem.Web.Component.Popover;

public interface IPopoverHandler
{
    Task InitializeAsync();

    Task SetupPopovers(string targetElementFilter);

    Task SetupPopover(PopoverDetail popoverDetail);

    Task UpdatePopover(string targetElementFilter, string action);

    Task RefreshReplacement(string contentSourceId, string headerSourceId);
}
