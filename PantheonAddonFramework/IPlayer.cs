using PantheonAddonFramework.Models;

namespace PantheonAddonFramework;

public interface IPlayer
{
    string Name { get; }
    
    int Level { get; }
    
    bool IsMale { get; }
    
    string Race { get; }
    
    string Class { get; }
    
    PlayerExperience? GetExperience();

    bool IsLocalPlayer { get; }
}