using UnityEngine;

public class InventoryBroker : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;

    public int TransferedMoney { get; private set; }
    public int TransferedStars { get; private set; }

    public void TransferInventoryData(LevelInventory levelInventory)
    {
        TransferedMoney = levelInventory.GetProfit(ItemType.MONEY);
        TransferedStars = levelInventory.GetProfit(ItemType.KEY);
        gameInventory.UpdateGameInventory(TransferedMoney, ItemType.MONEY);
        gameInventory.UpdateGameInventory(TransferedStars, ItemType.KEY);
    }
}
