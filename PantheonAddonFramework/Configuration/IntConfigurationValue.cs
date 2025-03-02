namespace PantheonAddonFramework.Configuration;

public class IntConfigurationValue : IConfigurationValue
{
    public string Name { get; }
    public string Description { get; }
    public int InitialValue { get; }
    public int MinValue { get; }
    public int MaxValue { get; }
    public int StepAmount { get; }
    public Action<int> OnValueChanged { get; }

    public IntConfigurationValue(string name, string description, int initialValue, int minValue, int maxValue, int stepAmount, Action<int> onValueChanged)
    {
        Name = name;
        Description = description;
        InitialValue = initialValue;
        MinValue = minValue;
        MaxValue = maxValue;
        StepAmount = stepAmount;
        OnValueChanged = onValueChanged;
    }
}