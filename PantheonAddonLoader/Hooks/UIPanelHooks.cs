using HarmonyLib;
using Il2Cpp;
using PantheonAddonFramework;
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
public class UIHandlePoolChanged
{
    private static void PostFix(UIPoolBar __instance, float current, float max)
    {
        if (__instance.name == "Health")
        {

        }
        if (__instance.name == "Mana")
        {

        }
    }
}