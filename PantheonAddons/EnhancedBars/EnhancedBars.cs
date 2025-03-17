using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;
using System.Xml.Linq;

namespace PantheonAddons.EnhancedBars;

[AddonMetadata("Enhanced Pool Bars", "xowis", "Makes the pools better")]
public sealed class EnhancedBars : Addon
{
    private IAddonPoolBar? _OffWindowPoolbar;
    private IAddonTextComponent? _OffWindowPoolbarText;

    public override void OnCreate()
    {
        WindowPanelEvents.OnOffensiveTargetReady.Subscribe(OnOffensiveTargetReady);
        WindowPanelEvents.OnOffTargetPoolbarChange.Subscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OnOffensiveTargetChanged.Subscribe(HandleOffensiveTargetPoolbar);
    }

    public override void Enable()
    {
        _OffWindowPoolbarText?.Enable(true);
    }

    public override void Disable()
    {
        _OffWindowPoolbarText?.Enable(false);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return new IConfigurationValue[]
        {
            new FloatConfigurationValue("Set Font Size", "Sets the font size for the pool bar overlays.", 18.0f, 10.0f, 72.0f, 1.0f, OnFontSizeChanged)
        };
    }

    private void OnFontSizeChanged(float obj)
    {
        _OffWindowPoolbarText?.SetFontSize(obj);
    }

    public override void Dispose()
    {
        WindowPanelEvents.OnOffensiveTargetReady.Unsubscribe(OnOffensiveTargetReady);
        WindowPanelEvents.OnOffTargetPoolbarChange.Unsubscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OnOffensiveTargetChanged.Unsubscribe(HandleOffensiveTargetPoolbar);

        _OffWindowPoolbarText?.Destroy();
    }

    private void OnOffensiveTargetReady(IAddonPoolBar poolbar)
    {
        _OffWindowPoolbar = poolbar;
        _OffWindowPoolbar.SetupWindow();       
    }

    private void HandleOffensiveTargetPoolbar(float percent)
    {
        if (_OffWindowPoolbarText == null)
        {
            _OffWindowPoolbarText = _OffWindowPoolbar?.AddTextComponent("");
            _OffWindowPoolbarText?.SetSize(500, 20);
            _OffWindowPoolbarText?.SetFontSize(18);
        }

        _OffWindowPoolbarText?.SetText(CreateText(percent));
    }

    private static string CreateText(float percent)
    {
        return $"{percent:F0}%";
    }
}