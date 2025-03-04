namespace PantheonAddonFramework.Models;

public interface IPlayer
{
    IPlayerStats Stats { get; }

    string Name { get; }

    int Level { get; }

    bool IsMale { get; }

    string Race { get; }

    string Class { get; }

    PlayerExperience? GetExperience();

    bool IsLocalPlayer { get; }
}