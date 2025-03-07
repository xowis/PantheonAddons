using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddons.EnhancedBars;

[AddonMetadata("Enhanced health bars Bar", "xowis", "Provides values on the bars")]
public sealed class EnhancedBars : Addon
{
    private float _originalHeight;
    private float _originalWidth;
    private IAddonWindow? _window;
    private IAddonTextComponent? _text;

    public override void OnCreate()
    {
        
    }

    public override void Enable()
    {
        _text?.Enable(true);
    }

    public override void Disable()
    {
        _text?.Enable(false);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        //WindowPanelEvents.OnExperienceBarReady.Unsubscribe(OnGroupMembersReady);

        _text?.Destroy();
    }

    private void OnGroupMembersReady(IAddonWindow window)
    {
        _originalHeight = window.Height;
        _originalWidth = window.Width;
        _window = window;

        _window.SetHeight(_originalHeight + 10);
        _window.SetWidth(_originalWidth / 2);

        _text = _window.AddTextComponent("0 / 0");
        _text.SetSize(500, 20);
    }

    private static string CreateText(PlayerExperience playerExperience)
    {
        return $"{playerExperience.Current:N0} / {playerExperience.ToNextLevel:N0} ({playerExperience.ExperiencePercentage * 100:F}%)";
    }
}