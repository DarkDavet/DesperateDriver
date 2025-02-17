using TMPro;
using UnityEngine;

public class UIMoneyWidget : MonoBehaviour, IUIWidget, IResetable
{
    [SerializeField] private TextMeshProUGUI textMoneyBalance;


    public void UpdateWidget(int amount)
    {
        textMoneyBalance.text = amount.ToString();
    }

    public void ResetObject()
    {
        textMoneyBalance.text = "0";
    }
}
