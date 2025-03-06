using HarmonyLib;
using Il2Cpp;
using PantheonAddonFramework.Models;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.Models;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(EntityPlayerGameObject), nameof(EntityPlayerGameObject.NetworkStart))]
public class PlayerNetworkStart
{
    private static void Postfix(EntityPlayerGameObject __instance)
    {
        // Fired in character select
        if (__instance.NetworkId.Value == 1)
        {
            return;
        }

        var player = new Player(__instance);
        
        if (player.IsLocalPlayer)
        {
            Globals.LocalPlayer = __instance;
            AddonLoader.LocalPlayerEvents.OnLocalPlayerEntered.Raise(player);
        }

        AddonLoader.PlayerEvents.OnPlayerAdded.Raise(player);
    }
}

[HarmonyPatch(typeof(Experience.Logic), nameof(Experience.Logic.SetExperience))]
public class ExperienceSetHook
{
    private static void Postfix(Experience.Logic __instance)
    {
        if (Globals.LocalPlayer?.Experience == __instance)
        {
            AddonLoader.LocalPlayerEvents.OnExperienceChanged.Raise(new PlayerExperience(__instance.CalculateCurrentExperienceIntoLevel(), __instance.CalculateExperienceRequiredToNextLevel(), __instance.CalculatePercentThroughCurrentLevel()));
        }
    }
}