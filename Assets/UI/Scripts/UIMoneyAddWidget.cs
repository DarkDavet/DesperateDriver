using TMPro;
using UnityEngine;

public class UIMoneyAddWidget : MonoBehaviour, IUIWidget
{
    [SerializeField] private TextMeshProUGUI textAddedMoney;
    [SerializeField] private UIMoneyAddPopupAnim animAdd;

    private int amountMoneyAdded;

    public void UpdateWidget(int amount)
    {
        amountMoneyAdded = amount;
        textAddedMoney.text = amountMoneyAdded.ToString();
        animAdd.PlayPopupAnimation();
    }
}
