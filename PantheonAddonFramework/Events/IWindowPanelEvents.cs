using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IAddonWindow> OnExperienceBarReady { get; }
    AddonEvent<IAddonWindow> OnWindowMoved { get; }
    AddonEvent<PoolBarData> OnPoolBarPlayerHealthChanged { get; }
    AddonEvent<PoolBarData> OnPoolBarPlayerManaChanged { get; }
}