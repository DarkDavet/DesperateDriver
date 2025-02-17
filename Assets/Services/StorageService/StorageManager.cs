using UnityEngine;

public class StorageManager : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;

    private IStorageService storageService;

    private const string KEY_GAME_INVENTORY = "Game Inventory";

    private void Start()
    {
        Application.targetFrameRate = 30;
        storageService = new JsonToFileStorageService();
        LoadGameData();
    }

    public void SaveGameData()
    {
        var data = gameInventory.PackGameInventoryData();
        storageService.Save(KEY_GAME_INVENTORY, data, success =>
        {
            if (success)
            {
                Debug.Log("Inventory saved successfully!");
            }
            else
            {
                Debug.LogError("Failed to save inventory.");

            }
        });
    }

    public void LoadGameData()
    {
        Debug.Log("Loading...");
        storageService.Load<GameInventoryData>(KEY_GAME_INVENTORY, data =>
        {
            if (data != null)
            {
                gameInventory.UnpackGameInventoryData(data);
                Debug.Log("Inventory loaded successfully.");

            }
            else
            {
                Debug.LogError("Failed to load inventory.");
            }
        });
    }
}
