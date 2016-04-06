using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UiLayer
{
    Background,
    Main,
    Overlay,
    Hud
}

public class Ui : MonoBehaviourSingleton<Ui>
{
    public RectTransform BackgroundLayer;
    public RectTransform MainLayer;
    public RectTransform OverlayLayer;
    public RectTransform HudLayer;

    private Dictionary<UiLayer, RectTransform> _layerDictionary;
    private readonly Dictionary<UiLayer, GameObject> _inputBlockerDictionary = new Dictionary<UiLayer, GameObject>();

    protected override bool DestroyOnLoad { get { return true; } }

    protected override void InitSingletonInstance()
    {
        _layerDictionary = new Dictionary<UiLayer, RectTransform>
        {
            { UiLayer.Background, BackgroundLayer},
            { UiLayer.Main, MainLayer},
            { UiLayer.Overlay, OverlayLayer},
            { UiLayer.Hud, HudLayer}
        };
    }

    protected override void DestroySingletonInstance()
    {
    }

    public void DisableInteractionOnLayersBelow(UiLayer uiLayer)
    {
        RectTransform layerTransform;
        if(_layerDictionary.TryGetValue(uiLayer, out layerTransform))
        {
            GameObject inputBlocker;
            if(_inputBlockerDictionary.TryGetValue(uiLayer, out inputBlocker) == false)
            {
                inputBlocker = new GameObject("Input Blocker");
                inputBlocker.transform.SetParent(layerTransform, false);
                inputBlocker.transform.SetAsFirstSibling();
                RectTransform rectTransform = inputBlocker.AddComponent<RectTransform>();
                rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;
                // inputBlocker.AddComponent<Button>();
                Image image = inputBlocker.AddComponent<Image>();
                image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                _inputBlockerDictionary.Add(uiLayer, inputBlocker);
            }
        }
    }

    public void ReenableInteractionOnLayersBelow(UiLayer uiLayer)
    {
        GameObject inputBlocker;
        if(_inputBlockerDictionary.TryGetValue(uiLayer, out inputBlocker))
        {
            _inputBlockerDictionary.Remove(uiLayer);
            Destroy(inputBlocker);
        }
    }
}
