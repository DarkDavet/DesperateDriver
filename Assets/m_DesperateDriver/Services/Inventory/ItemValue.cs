using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [SerializeField] private LevelInventory inventory;
    public int Value { get; set; }

    private void OnDestroy()
    {
        if (Value != 0)
        {
            inventory.CollectItems(Value, ItemType.MONEY);
        }
    }
}
