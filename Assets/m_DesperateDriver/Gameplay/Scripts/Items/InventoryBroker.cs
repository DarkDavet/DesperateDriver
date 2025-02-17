using UnityEngine;

public class InventoryBroker : MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private GameInventory gameInventory;

    public void TransferInventoryData()
    {
        int transferedMoney = levelInventory.CheckItemsAmount(ItemType.MONEY);
        int transferedKeys = levelInventory.CheckItemsAmount(ItemType.KEY);

        gameInventory.UpdateGameInventory(transferedMoney, ItemType.MONEY);
        gameInventory.UpdateGameInventory(transferedKeys, ItemType.KEY);
    }
}
