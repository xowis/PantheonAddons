namespace PantheonAddonFramework.Events;

public interface IPlayerEvents
{
    AddonEvent<IPlayer> OnPlayerAdded { get; }
    AddonEvent<IPlayer> OnPlayerRemoved { get; }
}