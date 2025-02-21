
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageManager : SingletonGlobal<StorageManager>
{
    [SerializeField] private GameInventory gameInventory;

    private IStorageService storageService;

    private const string KEY_GAME_INVENTORY = "Game Inventory";
    private const string KEY_LEVEL_STACKS = "Level Stacks";
    private const string KEY_CAR_COLORS = "Car Colors";
    private const string KEY_CAR_MATERIAL = "Car Material";

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
                Debug.LogError("Failed to save current car material.");

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
                Debug.LogError("Failed to load current car material.");
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
                Debug.LogError("Failed to save goods' status.");

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
                Debug.LogError("Failed to load goods' status.");
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
                Debug.LogError("Failed to save car colors.");

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
                Debug.LogError("Failed to load car colors");
            }

        });
    }
}
