using UnityEngine;

public class LevelInventoryBroker : MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    public void TransferCollectProcess(int value, string itemType)
    {
        levelInventory.CollectItems(value, itemType);
    }
}
