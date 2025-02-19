using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Game Inventory")]
public class GameInventory: ScriptableObject 
{
    public event Action<int, string> OnInventoryChanged;

    [ReadOnly, SerializeField] private int money;
    [ReadOnly, SerializeField] private int keys;
    private int Money { get => money; set => money = Mathf.Clamp(value, 0, 1000000); }
    private int Keys { get => keys; set => keys = Mathf.Clamp(value, 0, 100); }

    public void Initialize()
    {
        DisplayItems(Money, ItemType.MONEY);
    }

    public void UpdateGameInventory(int amount, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                Money += amount;
                break;
            case ItemType.KEY:
                Keys += amount;
                break;
        }
    }

    public GameInventoryData PackGameInventoryData()
    {
        return new GameInventoryData { Money = this.Money, Keys = this.Keys };
    }

    public void UnpackGameInventoryData(GameInventoryData data)
    {
        Money = data.Money;
        Keys = data.Keys;
    }

    private void DisplayItems(int amount, string itemType)
    {
        OnInventoryChanged?.Invoke(amount, itemType);
    }

    public bool RequestPayment(int cost)
    {
        if (cost <= Money)
        {
            Money -= cost;
            DisplayItems(Money, ItemType.MONEY);
            StorageManager.Instance.SaveGameInventoryData();
            return true;
        }
        return false;
    }
}

[Serializable]
public class GameInventoryData
{
    public int Money;
    public int Keys;
}
