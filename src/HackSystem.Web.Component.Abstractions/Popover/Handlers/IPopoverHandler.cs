namespace HackSystem.Web.Component.Popover;

public interface IPopoverHandler
{
    Task InitializeAsync();

    Task SetupPopover(string targetElementFilter);

    Task SetupPopover(PopoverDetail popoverDetail);

    Task UpdatePopover(string targetElementFilter, string action);
}
