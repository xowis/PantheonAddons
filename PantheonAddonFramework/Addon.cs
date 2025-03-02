using PantheonAddonFramework.Components;
using PantheonAddonFramework.Events;

namespace PantheonAddonFramework;

public abstract class Addon : IDisposable
{
    public ILogger Logger { get; set; }
    public IConfiguration Configuration { get; set; }
    public IWindowPanelEvents WindowPanelEvents { get; set; }
    public ILocalPlayerEvents LocalPlayerEvents { get; set; }
    
    public string Name { get; set; }
    public bool Enabled { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }

    public abstract void OnCreate();
    public abstract void Enable();
    public abstract void Disable();
    public abstract void AddConfiguration();

    public abstract void Dispose();
}