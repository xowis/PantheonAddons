using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IAddonWindow> OnExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> OnWindowMoved { get; } = new();
    public AddonEvent<PoolBarData> OnPoolBarPlayerHealthChanged { get; } = new();
    public AddonEvent<PoolBarData> OnPoolBarPlayerManaChanged { get; } = new();
}