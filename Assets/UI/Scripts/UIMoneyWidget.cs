using TMPro;
using UnityEngine;

public class UIMoneyWidget : MonoBehaviour, IUIWidget
{
    [SerializeField] private TextMeshProUGUI textMoneyBalance;

    private int countMoneyBalance;

    public void UpdateWidget(int amount)
    {
        countMoneyBalance += amount;
        textMoneyBalance.text = countMoneyBalance.ToString();
    }
}
