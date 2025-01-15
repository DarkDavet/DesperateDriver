using UnityEngine;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] private UIMoneyAddWidget uiMoneyAddedWidget;
    [SerializeField] private UIMoneyRemoveWidget uiMoneyRemoveWidget;
 

    public void OnMoneyAdded(int addedAmount)
    {
        uiMoneyAddedWidget.UpdateWidget(addedAmount);
    }

    public void OnMoneyRemoved(int removedAmount)
    {
        uiMoneyRemoveWidget.UpdateWidget(removedAmount);
    }
}
