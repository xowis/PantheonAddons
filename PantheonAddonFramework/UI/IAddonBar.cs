namespace PantheonAddonFramework.UI;

public interface IAddonBar
{
    float Height { get; }
    float Width { get; }
    float X { get; }
    float Y { get; }
    void SetHeight(float newHeight);
    void SetWidth(float newWidth);
    void SetPosition(float newX, float newY);

    IAddonTextComponent AddTextComponent(string initialText);
}