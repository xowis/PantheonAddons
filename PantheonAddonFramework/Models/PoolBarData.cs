using Il2Cpp;
using PantheonAddonFramework.UI;

namespace PantheonAddonFramework.Models;

public class PoolBarData
{
    public IAddonPoolBar PoolBar { get; }
    public float Current { get; }
    public float Max { get; }

    public PoolBarData(IAddonPoolBar poolBar, float current, float max)
    {
        PoolBar = poolBar;
        Current = current;
        Max = max;
    }
}