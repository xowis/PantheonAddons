using Il2Cpp;
using Il2CppPantheonPersist;
using Il2CppTMPro;
using PantheonAddonFramework.UI;
using SevenZip.Compression.LZ;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.UI;

public class AddonPoolBar : IAddonPoolBar
{
    private UIPoolBar _poolbar;
    private RectTransform _rectTransform;
    private PoolType _poolType;

    public AddonPoolBar(UIPoolBar poolbar)
    {
        _poolbar = poolbar;
        _rectTransform = poolbar.GetComponent<RectTransform>();
    }

    public float Height => _rectTransform.sizeDelta.y;
    public float Width => _rectTransform.sizeDelta.x;
    public float X => _rectTransform.transform.position.x;
    public float Y => _rectTransform.transform.position.y;

    public IAddonTextComponent AddTextComponent(string initialText)
    {
        var go = new GameObject("Test");
        go.transform.SetParent(_poolbar.transform);

        var textComponent = go.AddComponent<TextMeshProUGUI>();
        textComponent.text = initialText;
        textComponent.fontSize = 14;
        textComponent.fontStyle = (FontStyles)FontStyle.Bold;
        textComponent.alignment = TextAlignmentOptions.Center;

        var rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);

        return new AddonTextComponent(textComponent);
    }

    public void Enable(bool enabled)
    {
        _poolbar.gameObject.SetActive(enabled);
    }

    public void Destroy()
    {
        Object.Destroy(_poolbar);
    }

    public void SetHeight(float height)
    {
        _rectTransform.sizeDelta = new Vector2(Width, height);
    }

    public void SetWidth(float newWidth)
    {
        _rectTransform.sizeDelta = new Vector2(newWidth, Height);
    }

    public void SetPosition(float newX, float newY)
    {
        _rectTransform.transform.position = new Vector2(newX, newY);
    }
}
