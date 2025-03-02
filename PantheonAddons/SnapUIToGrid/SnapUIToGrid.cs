using PantheonAddonFramework;
using PantheonAddonFramework.Components;

namespace PantheonAddons.SnapUIToGrid;

[AddonMetadata("Snap UI to grid", "Roast", "Snaps windows to a grid when moving")]
public sealed class SnapUIToGrid : Addon
{
    private bool _isSnapping;
    
    public override void OnCreate()
    {
        WindowPanelEvents.OnWindowMoved.Subscribe(OnWindowMoved);
    }

    private void OnWindowMoved(IAddonWindow window)
    {
        if (!_isSnapping)
        {
            return;
        }
        
        var windowX = window.X;
        var windowY = window.Y;

        var newX = GetNearestMultiple(windowX, 5.0f);
        var newY = GetNearestMultiple(windowY, 5.0f);
        
        window.SetPosition(newX, newY);
    }

    public override void Enable()
    {
        _isSnapping = true;
    }

    public override void Disable()
    {
        _isSnapping = false;
    }

    public override void AddConfiguration()
    {
        
    }

    public override void Dispose()
    {
        WindowPanelEvents.OnWindowMoved.Unsubscribe(OnWindowMoved);
    }
    
    private static float GetNearestMultiple(float number, float multiple)
    {
        return (float)(Math.Round(number / multiple) * multiple);
    }
}