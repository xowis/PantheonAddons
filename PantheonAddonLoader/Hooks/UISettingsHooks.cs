using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
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
        // TODO: Move this out of the hook in to another evented class
        if (__instance.transform.childCount < 11)
        {
            return;
        }
        
        var tabButtons = __instance.transform.GetChild(3);

        var otherButton = tabButtons.GetChild(5);

        var tabGeneral = __instance.transform.GetChild(4);
        var generalLayout = tabGeneral.GetChild(0);
        var spacer = generalLayout.GetChild(0);
        var tooltipScaleSlider = generalLayout.GetChild(11);
        
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

        var buttonToCopy = generalLayout.GetChild(1);
        
        foreach (var addon in AddonLoader.LoadedAddons)
        {
            var configurationValue = new BoolConfigurationValue($"Enable {addon.Name}", "", true, b =>
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
            
            SetupCustomToggle(addon, configurationValue, modTabPage.GetChild(0), buttonToCopy);

            foreach (var configuration in addon.GetConfiguration())
            {
                switch (configuration)
                {
                    case FloatConfigurationValue floatConfigurationValue:
                        SetupCustomSlider(addon, floatConfigurationValue, modTabPage.GetChild(0), tooltipScaleSlider);
                        break;
                    case IntConfigurationValue intConfigurationValue:
                        SetupCustomSlider(addon, intConfigurationValue, modTabPage.GetChild(0), tooltipScaleSlider);
                        break;
                    case BoolConfigurationValue boolConfigurationValue:
                        SetupCustomToggle(addon, boolConfigurationValue, modTabPage.GetChild(0), buttonToCopy);
                        break;
                }
            }
            
            GameObject.Instantiate(spacer, spacer.transform.position, spacer.transform.rotation, modTabPage.GetChild(0));
        }
    }
    
    private static void SetupCustomToggle(Addon addon, BoolConfigurationValue boolConfigurationValue, Transform parent, Transform buttonToCopy)
    {
        var copy = GameObject.Instantiate(buttonToCopy, buttonToCopy.position, buttonToCopy.rotation, parent);
        copy.name = $"Toggle_{addon.Name}_{boolConfigurationValue.Name}";
        copy.GetChild(0).GetComponent<TextMeshProUGUI>().text = boolConfigurationValue.Name;
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigBool>());
        
        var toggleComp = copy.GetComponent<Toggle>();
        toggleComp.isOn = boolConfigurationValue.InitialValue;
        toggleComp.onValueChanged.RemoveAllListeners();
        toggleComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            boolConfigurationValue.OnValueChanged(toggleComp.isOn);
        })));
    }

    private static void SetupCustomSlider(Addon addon, FloatConfigurationValue configurationValue, Transform parent, Transform sliderToCopy)
    {
        var copy = GameObject.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configurationValue.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configurationValue.Name} - {configurationValue.InitialValue:F1}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configurationValue.MinValue;
        sliderComp.maxValue = configurationValue.MaxValue;
        sliderComp.value = configurationValue.InitialValue;
        sliderComp.wholeNumbers = false;
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configurationValue.StepAmount);
            textComp.text = $"{configurationValue.Name} - {sliderComp.value:F1}";
            
            configurationValue.OnValueChanged(sliderComp.value);
        })));

        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configurationValue.Name;
        tooltip.TooltipText = configurationValue.Description;
    }
    
    // TODO: Clean up duplicate code between this method and one above
    private static void SetupCustomSlider(Addon addon, IntConfigurationValue configurationValue, Transform parent, Transform sliderToCopy)
    {
        var copy = GameObject.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configurationValue.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configurationValue.Name} - {configurationValue.InitialValue}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configurationValue.MinValue;
        sliderComp.maxValue = configurationValue.MaxValue;
        sliderComp.value = configurationValue.InitialValue;
        sliderComp.wholeNumbers = true;
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configurationValue.StepAmount);
            textComp.text = $"{configurationValue.Name} - {sliderComp.value}";
            
            configurationValue.OnValueChanged((int)sliderComp.value);
        })));

        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configurationValue.Name;
        tooltip.TooltipText = configurationValue.Description;
    }
    
    private static float GetNearestMultiple(float number, float multiple)
    {
        return (float)(Math.Round(number / multiple) * multiple);
    }
}