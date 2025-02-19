
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageManager : SingletonGlobal<StorageManager>
{
    [SerializeField] private GameInventory gameInventory;

    private IStorageService storageService;

    private const string KEY_GAME_INVENTORY = "Game Inventory";
    private const string KEY_GOODS = "Goods";

    private void Start()
    {
        storageService = new JsonToFileStorageService();
        LoadGameInventoryData();
    }

    public void SaveGameInventoryData()
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

    public void LoadGameInventoryData()
    {
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

    public void SaveGoodsData(List<Product> goods)
    {
        var data = new List<ProductData>();
        foreach (var item in goods)
        {
            data.Add(item.PackProductData());
        }

        storageService.Save(KEY_GOODS, data, success =>
        {
            if (success)
            {
                Debug.Log("Goods' status saved successfully!");
            }
            else
            {
                Debug.LogError("Failed to save goods' status.");

            }
        });
    }

    public void LoadGoodsData(List<Product> goods)
    {
        storageService.Load<List<ProductData>>(KEY_GOODS, data =>
        {
            if (data != null)
            {
                for (int i = 0; i < goods.Count; i++)
                {
                    goods[i].UnpackProductData(data[i]);
                }

                Debug.Log("Goods' status loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load goods' status.");
            }

        });
    }
}
