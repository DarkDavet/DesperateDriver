using UnityEngine;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;

    [SerializeField] private UIMoneyAddWidget uiMoneyAddedWidget;
    [SerializeField] private UIMoneyRemoveWidget uiMoneyRemoveWidget;
    [SerializeField] private UIStatusStateWidget uiStatusStateWidget;

    private void Start()
    {
        levelInventory.OnInventoryIntialized += SetupPlayerWidgets;
    }

    private void SetupPlayerWidgets(int maxValue, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                uiStatusStateWidget.SetupSliderValue(maxValue);
                break;
            case ItemType.KEY:
                break;
        }
    }

    public void OnMoneyAdded(int addedAmount)
    {
        uiMoneyAddedWidget.UpdateWidget(addedAmount);
        uiStatusStateWidget.UpdateWidget(addedAmount);
    }

    public void OnMoneyRemoved(int removedAmount)
    {
        uiMoneyRemoveWidget.UpdateWidget(removedAmount);
        uiStatusStateWidget?.UpdateWidget(removedAmount);
    }

   
}
