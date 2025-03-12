using TMPro;
using UnityEngine;

public class UIStarsWidget : MonoBehaviour, IUIWidget
{
    [SerializeField] private TextMeshProUGUI textStarsBalance;
    public void UpdateWidget(int amount)
    {
        textStarsBalance.text = amount.ToString();
    }
}
