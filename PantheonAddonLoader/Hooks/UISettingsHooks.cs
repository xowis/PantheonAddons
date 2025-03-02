using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using PantheonAddonFramework;
using PantheonAddonLoader.AddonManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UISettings), nameof(UISettings.Awake))]
public class UISettingsHooks
{
    private static void Postfix(UISettings __instance)
    {
        if (__instance.transform.childCount < 11)
        {
            return;
        }
        
        var tabButtons = __instance.transform.GetChild(3);

        var otherButton = tabButtons.GetChild(5);

        var tabGeneral = __instance.transform.GetChild(4);
        var tabOther = __instance.transform.GetChild(9);
        
        var newButton = Object.Instantiate(otherButton, otherButton.position, otherButton.rotation, tabButtons);
        newButton.name = "Addons";
        var tabPageButton = newButton.GetComponent<UITabPageButton>();
        tabPageButton.Text.text = "Addons";
        
        var tabPageOther = __instance.transform.GetChild(9);
        var modTabPage = Object.Instantiate(tabPageOther, tabPageOther.position, tabPageOther.rotation,
            __instance.transform);
        modTabPage.name = "TabPage_Addons";
        
        var newTabPageLayout = modTabPage.GetChild(0);
        
        for (var i = 0; i < newTabPageLayout.childCount; i++)
        {
            Object.Destroy(newTabPageLayout.GetChild(i).gameObject);
        }
        
        var uiTabPage = modTabPage.GetComponent<UITabPage>();
        uiTabPage.TabButton = tabPageButton;
        tabPageButton.TabPage = uiTabPage;
        
        var parentRect = __instance.GetComponent<RectTransform>();
        var parentSize = parentRect.sizeDelta;
        parentRect.sizeDelta =
            new Vector2(parentSize.x + newButton.GetComponent<RectTransform>().sizeDelta.x, parentSize.y);

        var buttonToCopy = tabGeneral.GetChild(0).GetChild(1);
        
        foreach (var addon in AddonLoader.LoadedAddons)
        {
            SetupCustomToggle(addon, modTabPage.GetChild(0), buttonToCopy, b =>
            {
                if (b)
                {
                    addon.Enable();
                }
                else
                {
                    addon.Disable();
                }
            });
        }
    }
    
    private static void SetupCustomToggle(Addon addon, Transform parent, Transform buttonToCopy, Action<bool> onToggle)
    {
        var copy = GameObject.Instantiate(buttonToCopy, buttonToCopy.position, buttonToCopy.rotation, parent);
        copy.name = $"Toggle_{addon.Name}";
        copy.GetChild(0).GetComponent<TextMeshProUGUI>().text = addon.Name;
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigBool>());
        
        var toggleComp = copy.GetComponent<Toggle>();
        toggleComp.isOn = true;
        toggleComp.onValueChanged.RemoveAllListeners();
        toggleComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            onToggle(toggleComp.isOn);
        })));
    }
}