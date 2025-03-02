using PantheonAddonFramework.Components;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IAddonWindow> OnExperienceBarReady { get; }
    AddonEvent<IAddonWindow> OnWindowMoved { get; }
}