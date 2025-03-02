using PantheonAddonFramework.Components;
using PantheonAddonFramework.Events;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IAddonWindow> OnExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> OnWindowMoved { get; } = new();
}