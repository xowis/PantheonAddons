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
    private IAddonPoolBar? _poolbar;
    private IAddonTextComponent? _text;

    public override void OnCreate()
    {
        WindowPanelEvents.OnPoolBarPlayerHealthChanged.Subscribe(OnPoolBarPlayerHealthChanged);
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
        WindowPanelEvents.OnPoolBarPlayerHealthChanged.Unsubscribe(OnPoolBarPlayerHealthChanged);

        _text?.Destroy();
    }

    private void OnPoolBarPlayerHealthChanged(PoolBarData data)
    {
        if (_poolbar is null)        
        {
            _poolbar = data.PoolBar;
            _originalHeight = data.PoolBar.Height;
            _originalWidth = data.PoolBar.Width;

            _poolbar.SetHeight(_originalHeight + 100);
            //_poolbar.SetWidth(_originalWidth / 2);

            _text = _poolbar.AddTextComponent("0 / 0");
            _text.SetSize(500, 15);            
        }

        _text?.SetText(CreateText(data));
    }

    private static string CreateText(PoolBarData data)
    {
        return $"{data.Current:N0} / {data.Max:N0}";
    }
}