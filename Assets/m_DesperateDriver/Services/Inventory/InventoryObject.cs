using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    private List<InventorySlot> Container = new List<InventorySlot>();
    public List<InventorySlot> Slots => Container;
    public event Action OnInventoryChanged;
    public int Count { get { return Container.Count; } }
    public InventorySlot this[int index] { get { return Container[index]; } }
    public int MAX_SLOTS = 3;

    [NonSerialized] public CoroutineUtil _coroutine;

    public bool Contains(ItemObject _item)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        Container.Clear();
        OnInventoryChanged?.Invoke();
    }

    public bool AddItem(ItemObject _item)
    {
        if (Container.Count >= MAX_SLOTS)
        {
            Debug.Log("Inventory is full. Cannot add new item: " + _item.name);
            return false; 
        }
        Container.Add(new InventorySlot(_item));
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void UseItem(ItemObject item)
    {
        Debug.Log($"Using item: {item.name}");
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == item)
            {
                Container[i].Use();
                Container.RemoveAt(i);
                OnInventoryChanged?.Invoke();
                return;
            }
        }
    }

    public void RemoveItem(int slotHash)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].GetHashCode() == slotHash)
            {
                Container.RemoveAt(i);
                OnInventoryChanged?.Invoke();
                break;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public InventorySlot(ItemObject _item)
    {
        item = _item;
    }

    public void Use()
    {
        item.Use();
    }
}
