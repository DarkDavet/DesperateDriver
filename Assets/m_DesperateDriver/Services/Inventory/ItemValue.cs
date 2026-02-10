using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [SerializeField] private string itemTag;
    public string ItemTag { get => itemTag; }
    public int Value { get; set; }

    private LevelInventoryBroker broker;
    //  private IceCreamStation iceCreamStation;

    private void Start()
    {
        broker = FindAnyObjectByType<LevelInventoryBroker>();
        LevelManager.OnLevelRestart.AddListener(HandleLevelRestart);
    }

    private void OnDestroy()
    {
        if (Value != 0)
        {
            Debug.Log("Destroy");
            broker.TransferCollectProcess(Value, ItemType.MONEY);
        }
        LevelManager.OnLevelRestart.RemoveListener(HandleLevelRestart);
    }

    private void HandleLevelRestart()
    {
        Debug.Log("illegal Destroy");
        Value = 0; // Prevent value transfer
        Destroy(gameObject);
    }
}
