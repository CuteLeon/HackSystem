using HackSystem.Web.ProgramPlatform.Abstractions.Enums;

namespace HackSystem.Web.ProgramPlatform.Components.ToastComponent;

public interface IToastContainer
{
    const string CascadingParameterName = "DesktopToastContainer";

    void PopToast(string title, string message, ToastIcons icon, bool autoHide = true, int hideDelay = 3000);
}
