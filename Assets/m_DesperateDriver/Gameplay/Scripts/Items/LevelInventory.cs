using System;
using Unity.VisualScripting;
using UnityEngine;
using System.ComponentModel;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Level Inventory")]
public class LevelInventory: ScriptableObject, IResetable
{
    public event Action<int, string> OnInventoryChanged;
    public event Action<int, string> OnInventoryIntialized;

    [Range(0, 200)]
    [SerializeField] private int lvlMoneyLimit;
    [Range(0, 10)]
    [SerializeField] private int lvlKeysLimit;

    [ReadOnly, SerializeField] private int tmp_Money;
    [ReadOnly, SerializeField] private int tmp_Keys;
    private int Tmp_Money { get => tmp_Money; set => tmp_Money = Mathf.Clamp(value, 0, lvlMoneyLimit); }
    private int Tmp_Keys { get => tmp_Keys; set => tmp_Keys = Mathf.Clamp(value, 0, lvlKeysLimit); }

    private int generated_Sum {  get; set; }

    public void Initialize()
    {
        OnInventoryIntialized?.Invoke(lvlMoneyLimit, ItemType.MONEY);
        OnInventoryIntialized?.Invoke(lvlKeysLimit, ItemType.KEY);

        tmp_Money = 0;
        DisplayItems(tmp_Money, ItemType.MONEY);

        tmp_Keys = 0;
        DisplayItems(tmp_Keys, ItemType.KEY);
    }

    public void CollectItems(int amount,string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                Tmp_Money += amount;
                DisplayItems(Tmp_Money, itemType);
                break;
            case ItemType.KEY:
                //generated_Sum = UnityEngine.Random.Range(1, 20);
                //Tmp_Keys += amount;
                //DisplayItems(Tmp_Money, itemType);
                break;
        }
    }

    private void DisplayItems(int amount, string itemType)
    {
        OnInventoryChanged?.Invoke(amount, itemType);
    }

    public int CheckItemsAmount(string itemType)
    {
        switch (itemType)
        {
            case ItemType.MONEY:
                return tmp_Money;
            case ItemType.KEY:
                return tmp_Keys;
        }
        return 0;
    }

    public void ResetObject()
    {
        tmp_Money = 0;
        DisplayItems(tmp_Money, ItemType.MONEY);

        tmp_Keys = 0;
        DisplayItems(tmp_Keys, ItemType.KEY);
    }
}
