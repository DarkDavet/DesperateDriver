using System.Collections.Generic;
using UnityEngine;

public class LevelInventoryData : SingletonGlobal<LevelInventoryData>
{
    [SerializeField] private List<LevelInventory> levelInventoriesList;

    public Dictionary<int, int> EarnedLevelMoney { get; private set; } = new Dictionary<int, int>();
    public Dictionary<int, int> EarnedLevelStars { get; private set; } = new Dictionary<int, int>();

    public void InitDictionary()
    {
        foreach (var level in levelInventoriesList)
        {
            level.PrepareForInit();
            level.OnInventoryReady += FillDictionary;
            level.OnInventorySaved += UpdateDictionary;
        }
    }
    public void FillDictionary(int key, int value, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                if (!EarnedLevelMoney.ContainsKey(key))
                {
                    EarnedLevelMoney.Add(key, value);
                }
                break;
            case ItemType.KEY:
                if (!EarnedLevelStars.ContainsKey(key))
                {
                    EarnedLevelStars.Add(key, value);
                }
                break;
        } 
    }

    public void UpdateDictionary(int key, int value, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                EarnedLevelMoney[key] = value;
                break;
            case ItemType.KEY:
                EarnedLevelStars[key] = value;
                break;
        }
        SaveLevelInventoryData();
    }

    public void SaveLevelInventoryData()
    {
        StorageManager.Instance.SaveLevelItemsData(Instance);
    }

    public LevelInventorySaveData PackLevelInventoryData()
    {
        var data = new LevelInventorySaveData()
        {
            EarnedLevelMoneyData = this.EarnedLevelMoney,
            EarnedLevelStarsData = this.EarnedLevelStars
        };
        return data;
    }

    public void UnpackLevelInventoryData(LevelInventorySaveData data)
    {
        this.EarnedLevelMoney = data.EarnedLevelMoneyData;
        this.EarnedLevelStars = data.EarnedLevelStarsData;

        foreach (var inventory in levelInventoriesList)
        {
            inventory.GetGlobalItemsFromData(EarnedLevelMoney, ItemType.MONEY);
            inventory.GetGlobalItemsFromData(EarnedLevelStars, ItemType.KEY);
        }
    }
}

public class LevelInventorySaveData
{
    public Dictionary<int, int> EarnedLevelMoneyData = new Dictionary<int, int>();
    public Dictionary<int, int> EarnedLevelStarsData = new Dictionary<int, int>();
}
