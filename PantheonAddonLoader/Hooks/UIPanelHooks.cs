using HarmonyLib;
using Il2Cpp;
using Il2CppLogicalGraphNodes;
using PantheonAddonFramework;
using PantheonAddonFramework.Models;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.UI;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UIWindowPanel), nameof(UIWindowPanel.Start))]
public class UIPanelHooks
{
    private static void Postfix(UIWindowPanel __instance)
    {
        if (__instance.name == "Panel_XpBar")
        {
            AddonLoader.WindowPanelEvents.OnExperienceBarReady.Raise(new AddonWindow(__instance));
        }
    }
}

[HarmonyPatch(typeof(UIPoolBar), nameof(UIPoolBar.HandlePoolChanged))]
public class UIPoolBarPoolChanged
{
    private static void Postfix(UIPoolBar __instance, float current, float max)
    {
        var pppName = __instance.transform.parent.parent.parent.name;
        var ppName = __instance.transform.parent.parent.name;
        if (ppName == "Player")
        {
            if (__instance.name == "Health")
            {
                AddonLoader.WindowPanelEvents.OnPoolBarPlayerHealthChanged.Raise(new PoolBarData(new AddonPoolBar(__instance), current, max));
            }
            if (__instance.name == "Mana")
            {
                AddonLoader.WindowPanelEvents.OnPoolBarPlayerManaChanged.Raise(new PoolBarData(new AddonPoolBar(__instance), current, max));
            }
        }



        if (ppName == "UITargetOfTarget")
        {

        }
        
    }
}