using TMPro;
using UnityEngine;

public class UIMoneyRemoveWidget: MonoBehaviour, IUIWidget
{
    [SerializeField] private TextMeshProUGUI textRemovedMoney;
    [SerializeField] private UIMoneyRemovePopupAnim animRemove;

    private int amountMoneyRemoved;

    public void UpdateWidget(int amount)
    {
        amountMoneyRemoved = amount;
        textRemovedMoney.text = amountMoneyRemoved.ToString();
        animRemove.PlayPopupAnimation();
    }
}
