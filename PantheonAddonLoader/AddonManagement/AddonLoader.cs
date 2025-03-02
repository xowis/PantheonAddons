using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonLoader.Events;

namespace PantheonAddonLoader.AddonManagement;

public class AddonLoader : MelonMod
{
    public const string ModVersion = "1.0.0";
    private static readonly string AddonsFolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PantheonAddons";
    public static readonly List<Addon> LoadedAddons = new();
    public static readonly WindowPanelEvents WindowPanelEvents = new();
    public static readonly LocalPlayerEvents LocalPlayerEvents = new();
    public static readonly PlayerEvents PlayerEvents = new();

    public override void OnInitializeMelon()
    {
        if (!Directory.Exists(AddonsFolderPath))
        {
            MelonLogger.Msg($"Creating addons folder {AddonsFolderPath}");
            Directory.CreateDirectory(AddonsFolderPath);
        }
        
        LoadAddons();
    }

    private static void LoadAddons()
    {
        LoadedAddons.Clear();
        
        MelonLogger.Msg($"Loading addons in {AddonsFolderPath}");
        
        foreach (var addonFile in Directory.GetFiles(AddonsFolderPath, "*.dll"))
        {
            var assembly = System.Reflection.Assembly.LoadFile(addonFile);
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(Addon)))
                {
                    continue;
                }
                
                var addon = ScriptActivator.ActivateAddon(type);
                if (addon != null)
                {
                    LoadedAddons.Add(addon);
                }
            }
        }
    }
}