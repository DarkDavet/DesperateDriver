using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelInventoryWidget: MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private UIMoneyWidget uiMoneyWidget;

    private void Start()
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
                Debug.Log("Key has added");
                break;
        }
    }
}
