
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageManager : SingletonGlobal<StorageManager>
{
    [SerializeField] private GameInventory gameInventory;

    private IStorageService storageService;

    private const string KEY_GAME_INVENTORY = "Game Inventory";
    private const string KEY_LEVEL_STACKS = "Level Stacks";
    private const string KEY_LEVEL_ITEMS = "Level Money";
    private const string KEY_CAR_COLORS = "Car Colors";
    private const string KEY_CAR_MATERIAL = "Car Material";
    private const string KEY_CAR_UPGRADES = "Car Upgrades";

    public void InitStorageManager()
    {
        if (storageService == null)
        {
            storageService = new JsonToFileStorageService();
        }
    }

    public void SaveGameInventoryData()
    {
        if (gameInventory == null)
        {
            Debug.LogError("gameInventory is null. Cannot save game inventory data.");
            return;
        }

        var data = gameInventory.PackGameInventoryData();

        if (storageService == null)
        {
            Debug.LogError("storageService is null. Initializing...");
            InitStorageManager();  // As a fallback
        }

        storageService.Save(KEY_GAME_INVENTORY, data, success =>
        {
            if (success)
            {
                Debug.Log("Inventory saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save inventory.");
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
                Debug.LogWarning("Failed to load inventory.");
            }
        });
    }
    public void SaveUpgradeData(UpgradeData upgradeData)
    {
        var data = upgradeData.PackUpgradeData();
        storageService.Save(KEY_CAR_UPGRADES, data, success =>
        {
            if (success)
            {
                Debug.Log("Upgrade levels saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save upgrade levels.");

            }
        });
    }

    public void LoadUpgradeData(UpgradeData upgradeData)
    {
        storageService.Load<UpgradeDataData>(KEY_CAR_UPGRADES, data =>
        {
            if (data != null)
            {
                upgradeData.UnpackUpgradeData(data);
                Debug.Log("Upgrade levels loaded successfully.");

            }
            else
            {
                Debug.LogWarning("Failed to load upgrade levels.");
            }
        });
    }

    public void SaveLevelItemsData(LevelInventoryData inventoryData)
    {
        var data = inventoryData.PackLevelInventoryData();
        storageService.Save(KEY_LEVEL_ITEMS, data, success =>
        {
            if (success)
            {
                Debug.Log("Level items saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save level items.");

            }
        });
    }

    public void LoadLevelItemsData(LevelInventoryData inventoryData)
    {
        storageService.Load<LevelInventorySaveData>(KEY_LEVEL_ITEMS, data =>
        {
            if (data != null)
            {
                inventoryData.UnpackLevelInventoryData(data);
                Debug.Log("Level items loaded successfully.");

            }
            else
            {
                Debug.LogWarning("Failed to load level items.");
            }
        });
    }

    public void SaveCarMaterialData(CarMatShop matShop)
    {
        var data = matShop.PackCarMaterialData();
        storageService.Save(KEY_CAR_MATERIAL, data, success =>
        {
            if (success)
            {
                Debug.Log("Current car material saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save current car material.");

            }
        });
    }

    public void LoadCarMaterialData(CarMatShop matShop)
    {
        storageService.Load<CarMaterialData>(KEY_CAR_MATERIAL, data =>
        {
            if (data != null)
            {
                matShop.UnpackCarMaterialData(data);
                Debug.Log("Current car material loaded successfully.");

            }
            else
            {
                Debug.LogWarning("Failed to load current car material.");
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

        storageService.Save(KEY_LEVEL_STACKS, data, success =>
        {
            if (success)
            {
                Debug.Log("Goods' status saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save goods' status.");

            }
        });
    }

    public void LoadGoodsData(List<Product> goods)
    {
        storageService.Load<List<ProductData>>(KEY_LEVEL_STACKS, data =>
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
                Debug.LogWarning("Failed to load goods' status.");
            }

        });
    }

    public void SaveCarColorsData(List<MatProduct> carColors)
    {
        var data = new List<MatProductData>();
        foreach (var item in carColors)
        {
            data.Add(item.PackProductData());
        }

        storageService.Save(KEY_CAR_COLORS, data, success =>
        {
            if (success)
            {
                Debug.Log("Car colors saved successfully!");
            }
            else
            {
                Debug.LogWarning("Failed to save car colors.");

            }
        });
    }

    public void LoadCarColorsData(List<MatProduct> carColors)
    {
        storageService.Load<List<MatProductData>>(KEY_CAR_COLORS, data =>
        {
            if (data != null)
            {
                for (int i = 0; i < carColors.Count; i++)
                {
                    carColors[i].UnpackProductData(data[i]);
                }

                Debug.Log("Car colors loaded successfully.");
            }
            else
            {
                Debug.LogWarning("Failed to load car colors");
            }

        });
    }
}
