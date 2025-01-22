using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusStateWidget : MonoBehaviour, IUIWidget, IResetable
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    private Color sliderColor;
    private TextMeshProUGUI titleName;

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

    public void SetStatusStateSetting(Color color, string title)
    {
        sliderColor = color;
        titleName.text = title;

        fillImage.color = color;
    }

    public void ResetObject()
    {
        slider.value = 0;
    }
}
