namespace PantheonAddonFramework.UI;

public interface IAddonPoolBar
{
    float Height { get; }
    float Width { get; }
    float X { get; }
    float Y { get; }
    void SetHeight(float newHeight);
    void SetWidth(float newWidth);
    void SetPosition(float newX, float newY);
    void SetupWindow();
    IAddonTextComponent AddTextComponent(string initialText);

    void Enable(bool enabled);
    void Destroy();
}