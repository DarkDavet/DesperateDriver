using UnityEngine;

public class UIGameInventoryWidget : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private UIMoneyWidget uiMoneyWidget;
    [SerializeField] private UIStarsWidget uiStarsWidget;

    public void Init()
    {
        gameInventory.OnInventoryChanged += Display;
    }
    private void OnDestroy()
    {
        gameInventory.OnInventoryChanged -= Display;
    }
    private void Display(int amount, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                uiMoneyWidget.UpdateWidget(amount);
                break;
            case ItemType.KEY:
                uiStarsWidget.UpdateWidget(amount);
                break;
        }
    }
}
