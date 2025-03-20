using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Game Inventory")]
public class GameInventory: ScriptableObject 
{
    public event Action<int, string> OnInventoryChanged;

    [ReadOnly, SerializeField] private int money;
    [ReadOnly, SerializeField] private int stars;
    private int Money { get => money; set => money = Mathf.Clamp(value, 0, 1000000); }
    private int Stars { get => stars; set => stars = Mathf.Clamp(value, 0, 100); }

    public void Initialize()
    {
        DisplayItems(Money, ItemType.MONEY);
        DisplayItems(Stars, ItemType.KEY);
    }

    public void UpdateGameInventory(int amount, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                Money += amount;
                break;
            case ItemType.KEY:
                Stars += amount;
                break;
        }
        DisplayItems(Money, ItemType.MONEY);
        DisplayItems(Stars, ItemType.KEY);
    }

    public GameInventoryData PackGameInventoryData()
    {
        var data = new GameInventoryData()
        {
            Money = this.Money,
            Stars = this.Stars
        };
        return data;
    }

    public void UnpackGameInventoryData(GameInventoryData data)
    {
        Money = data.Money;
        Stars = data.Stars;
    }

    private void DisplayItems(int amount, string itemType)
    {
        OnInventoryChanged?.Invoke(amount, itemType);
    }

    public bool RequestPayment(int cost, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                if (cost <= Money)
                {
                    Money -= cost;
                    DisplayItems(Money, ItemType.MONEY);
                    Debug.Log($"Money NOW!!!: {Money}");
                    StorageManager.Instance.SaveGameInventoryData();
                    return true;
                }
                break;
            case ItemType.KEY:
                if (cost <= Stars)
                {
                    Stars -= cost;
                    DisplayItems(Stars, ItemType.KEY);
                    Debug.Log($"Keys NOW!!!: {Stars}");
                    StorageManager.Instance.SaveGameInventoryData();
                    return true;
                }
                break;
        }
        return false;
    }
}

[Serializable]
public class GameInventoryData
{
    public int Money;
    public int Stars;
}
