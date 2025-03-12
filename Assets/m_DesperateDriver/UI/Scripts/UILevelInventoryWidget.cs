using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelInventoryWidget: MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private UIMoneyWidget uiMoneyWidget;
    [SerializeField] private UIStarsWidget uiStarsWidget;

    public void Init()
    {
        levelInventory.OnInventoryChanged += Display;
    }
    private void OnDestroy()
    {
        levelInventory.OnInventoryChanged -= Display;
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
