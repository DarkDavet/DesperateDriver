using System;
using Unity.VisualScripting;
using UnityEngine;
using System.ComponentModel;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Level Inventory")]
public class LevelInventory : ScriptableObject, IResetable
{
    public event Action<int, string> OnInventoryChanged;
    public event Action<int, string> OnInventoryIntialized;
    public event Action<int, int, string> OnInventoryReady;
    public event Action<int, int, string> OnInventorySaved;
    public event Action<int> OnItemsAdded;
    public event Action<int> OnItemsRemoved;

    [SerializeField] private int id;

    public int Id { get => id; }

    [Range(0, 500)]
    public int lvlMoneyLimit;
    [Range(0, 20)]
    public int lvlStarsLimit;

    [ReadOnly, SerializeField] private int tmp_Money;
    [ReadOnly, SerializeField] private int glb_Money;

    [Range(0, 500)][SerializeField] private int firstLimit;
    [Range(0, 500)][SerializeField] private int secondLimit;
    [Range(0, 500)][SerializeField] private int thirdLimit;

    public int FirstLimit { get => firstLimit; private set => firstLimit = Mathf.Clamp(value, 0, lvlMoneyLimit); }
    public int SecondLimit { get => secondLimit; private set => secondLimit = Mathf.Clamp(value, 0, lvlMoneyLimit); }
    public int ThirdLimit { get => thirdLimit; private set => thirdLimit = Mathf.Clamp(value, 0, lvlMoneyLimit); }


    [ReadOnly, SerializeField] private int tmp_Stars;
    [ReadOnly, SerializeField] private int glb_Stars;
    [ReadOnly, SerializeField] private bool isFullCompleted = false;
    public int Tmp_Money { get => tmp_Money; private set => tmp_Money = Mathf.Clamp(value, 0, lvlMoneyLimit); }
    public int Glb_Money { get => glb_Money; private set => glb_Money = Mathf.Clamp(value, 0, lvlMoneyLimit); }
    public int Tmp_Stars { get => tmp_Stars; private set => tmp_Stars = Mathf.Clamp(value, 0, lvlStarsLimit); }
    public int Glb_Stars { get => glb_Stars; private set => glb_Stars = Mathf.Clamp(value, 0, lvlStarsLimit); }

    private int generated_Sum { get; set; }

    public void Initialize()
    {
        OnInventoryReady?.Invoke(id, Glb_Money, ItemType.MONEY);
        OnInventoryReady?.Invoke(id, Glb_Stars, ItemType.KEY);
    }

    public void OnLevelStart()
    {
        OnInventoryIntialized?.Invoke(lvlMoneyLimit, ItemType.MONEY);
        OnInventoryIntialized?.Invoke(lvlStarsLimit, ItemType.KEY);

        Tmp_Money = 30;
        DisplayItems(Tmp_Money, ItemType.MONEY);

        Tmp_Stars = 0;
        DisplayItems(Tmp_Stars, ItemType.KEY);
    }

    public void CollectItems(int amount, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                Tmp_Money += amount;
                DisplayItems(Tmp_Money, itemType);
                OnItemsAdded?.Invoke(amount);
                break;
            case ItemType.KEY:
                Tmp_Stars += amount;
                DisplayItems(Tmp_Stars, itemType);
                break;
        }
    }

    public void GetGlobalItemsFromData(Dictionary<int, int> data, string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                Glb_Money = data[id];
                break;
            case ItemType.KEY:
                Glb_Stars = data[id];
                break;
        }
    }
    public int GetProfit(string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                int profitMoney = Tmp_Money - Glb_Money;
                if (profitMoney > 0)
                {
                    Glb_Money = Tmp_Money;
                    return profitMoney;
                }
                break;
            case ItemType.KEY:
                int profitStar = Tmp_Stars - Glb_Stars;
                if (profitStar > 0)
                {
                    Glb_Stars = Tmp_Stars;
                    return profitStar;
                }
                break;
        }
        return 0;

    }

    private void DisplayItems(int amount, string itemType)
    {
        OnInventoryChanged?.Invoke(amount, itemType);
    }

    public bool RequestPayment(int cost)
    {
        if (cost <= Tmp_Money)
        {
            Tmp_Money -= cost;
            DisplayItems(Tmp_Money, ItemType.MONEY);
            OnItemsRemoved?.Invoke(cost);
            return true;
        }
        Tmp_Money = 0;
        return false;
    }

    public void SetGlobalData()
    {
        FilterData();
        OnInventorySaved?.Invoke(id, Glb_Money, ItemType.MONEY);
        OnInventorySaved?.Invoke(id, Glb_Stars, ItemType.KEY);
    }

    private void FilterData()
    {
        if (Tmp_Money > Glb_Money)
        {
            Glb_Money = Tmp_Money;
        }
        if (Tmp_Stars > Glb_Stars)
        {
            Glb_Stars = Tmp_Stars;
        }
    }

    public void ResetObject()
    {
        OnInventoryChanged = null;
        OnInventoryIntialized = null;
        OnInventoryReady = null;
        OnInventorySaved = null;
        OnItemsAdded = null;
        OnItemsRemoved = null;

        Tmp_Money = 30;
        DisplayItems(Tmp_Money, ItemType.MONEY);

        Tmp_Stars = 0;
        DisplayItems(Tmp_Stars, ItemType.KEY);
    }
}
