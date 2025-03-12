using UnityEngine;

public class ItemValue : MonoBehaviour
{
    public int Value { get; set; }

    private LevelInventoryBroker broker;

    private void Start()
    {
        broker = FindAnyObjectByType<LevelInventoryBroker>();
    }

    private void OnDestroy()
    {
        if (Value != 0)
        {
            broker.TransferCollectProcess(Value, ItemType.MONEY);
        }
    }
}
