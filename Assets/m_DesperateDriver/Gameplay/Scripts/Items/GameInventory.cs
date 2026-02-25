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
                    Debug.Log($"Stars (GI): {Money}");
                    Debug.Log($"Requested Price (Money): {Money}");
                    return true;
                }
                break;
            case ItemType.KEY:
                if (cost <= Stars)
                {
                    Debug.Log($"Stars (GI): {Stars}");
                    Debug.Log($"Requested Price (Stars): {Stars}");
                    return true;
                }
                break;
        }

        return false;
    }

    public void ProcessPayment(int cost, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                if (cost <= Money)
                {
                    Money -= cost;
                    DisplayItems(Money, ItemType.MONEY);
                    StorageManager.Instance.SaveGameInventoryData();
                }
                break;
            case ItemType.KEY:
                if (cost <= Stars)
                {
                    Stars -= cost;
                    DisplayItems(Stars, ItemType.KEY);
                    StorageManager.Instance.SaveGameInventoryData();
                }
                break;
        }
    }
}

[Serializable]
public class GameInventoryData
{
    public int Money;
    public int Stars;
}
