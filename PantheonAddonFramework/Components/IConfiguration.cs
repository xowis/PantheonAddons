namespace PantheonAddonFramework.Components;

public interface IConfiguration
{
    void Add<T>(string name, T initialValue);
}