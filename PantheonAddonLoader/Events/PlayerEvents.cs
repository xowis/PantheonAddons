using PantheonAddonFramework;
using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public class PlayerEvents : IPlayerEvents
{
    public AddonEvent<IPlayer> OnPlayerAdded { get; } = new();
    public AddonEvent<IPlayer> OnPlayerRemoved { get; } = new();
}