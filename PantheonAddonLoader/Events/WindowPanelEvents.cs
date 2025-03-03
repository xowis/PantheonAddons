using PantheonAddonFramework.Events;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IAddonWindow> OnExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> OnWindowMoved { get; } = new();
}