using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusStateWidget : MonoBehaviour, IUIWidget, IResetable
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image handeImage;
    [SerializeField] private TextMeshProUGUI titleName;

    private Color stateColor;

    public void SetupSliderValue(int maxValue)
    {
        slider.value = 0;
        slider.maxValue = maxValue;
    }
    public void UpdateWidget(int amount)
    {
        if (slider.value >= 0)
        {
            slider.value += amount;
        }
    }

    public void SetStatusStateSettings(Color color, string title)
    {
        stateColor = color;
        titleName.text = title;
        titleName.color = stateColor;

        fillImage.color = stateColor;
        handeImage.color = stateColor;
    }

    public void ResetObject()
    {
        slider.value = 0;
    }
}
