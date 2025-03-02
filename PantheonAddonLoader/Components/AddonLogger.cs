using MelonLoader;
using PantheonAddonFramework.Components;

namespace PantheonAddonLoader.Components;

public class AddonLogger : ILogger
{
    public void Log(string message)
    {
        MelonLogger.Msg(message);
    }
}