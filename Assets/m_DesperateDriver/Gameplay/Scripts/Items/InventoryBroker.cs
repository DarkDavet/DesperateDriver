using UnityEngine;

public class InventoryBroker : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;

    public void TransferInventoryData(LevelInventory levelInventory)
    {
        int transferedMoney = levelInventory.GetProfit(ItemType.MONEY);
        int transferedStars = levelInventory.GetProfit(ItemType.KEY);
        gameInventory.UpdateGameInventory(transferedMoney, ItemType.MONEY);
        gameInventory.UpdateGameInventory(transferedStars, ItemType.KEY);
    }
}
