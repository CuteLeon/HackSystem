namespace HackSystem.Web.Component.Popover;

public interface IPopoverHandler
{
    Task InitializeAsync();

    Task SetupPopovers(string targetElementFilter);

    Task<string?> SetupPopover(PopoverDetail popoverDetail);

    Task UpdatePopover(string targetElementFilter, string action);

    Task RefreshContent(string popoverId, string replacementTargetId, string originSourceId);
}
