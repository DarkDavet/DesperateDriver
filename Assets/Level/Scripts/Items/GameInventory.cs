using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Game Inventory")]
public class GameInventory: ScriptableObject 
{
    [ReadOnly, SerializeField] private int money;
    [ReadOnly, SerializeField] private int keys;
    private int Money { get => money; set => money = Mathf.Clamp(value, 0, 1000000); }
    private int Keys { get => keys; set => keys = Mathf.Clamp(value, 0, 100); }

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
        return new GameInventoryData { Money = this.money, Keys = this.keys };
    }

    public void UnpackGameInventoryData(GameInventoryData data)
    {
        this.money = data.Money;
        this.keys = data.Keys;
    }
}

[Serializable]
public class GameInventoryData
{
    public int Money;
    public int Keys;
}
