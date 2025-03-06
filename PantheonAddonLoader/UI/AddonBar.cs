using Il2Cpp;
using Il2CppTMPro;
using PantheonAddonFramework.UI;
using UnityEngine;

namespace PantheonAddonLoader.UI;

public class AddonBar : IAddonBar
{
    private UIPoolBar _poolbar;
    private RectTransform _rectTransform;

    public AddonBar(UIPoolBar poolbar)
    {
        _poolbar = poolbar;
        _rectTransform = poolbar.GetComponent<RectTransform>();
    }

    public float Height => _rectTransform.sizeDelta.y;
    public float Width => _rectTransform.sizeDelta.x;
    public float X => _rectTransform.transform.position.x;
    public float Y => _rectTransform.transform.position.y;

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

    public IAddonTextComponent AddTextComponent(string initialText)
    {
        var go = new GameObject("Test");
        go.transform.SetParent(_poolbar.transform);

        var textComponent = go.AddComponent<TextMeshProUGUI>();
        textComponent.text = initialText;
        textComponent.fontSize = 18;
        textComponent.alignment = TextAlignmentOptions.Center;

        var rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);

        return new AddonTextComponent(textComponent);
    }
}