namespace PantheonAddonFramework.Configuration;

public class BoolConfigurationValue : IConfigurationValue
{
    public string Name { get; }
    public string Description { get; }
    public bool InitialValue { get; }
    public Action<bool> OnValueChanged { get; }

    public BoolConfigurationValue(string name, string description, bool initialValue, Action<bool> onValueChanged)
    {
        Name = name;
        Description = description;
        OnValueChanged = onValueChanged;
        InitialValue = initialValue;
    }
}