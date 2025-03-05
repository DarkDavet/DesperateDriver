using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private UpgradeSetup upgradeSetup;
    public InventoryObject inventory;
    public int MAX_SLOTS = 3;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<int, GameObject> itemsDisplayed = new Dictionary<int, GameObject>();
    Dictionary<GameObject, int> placementMap = new Dictionary<GameObject, int>();
    HashSet<int> occupiedPlaces = new HashSet<int>();

    void Start()
    {
        inventory.OnInventoryChanged += UpdateDisplay;

        if (upgradeSetup != null)
        {
            if (upgradeSetup.capacity > 0)
            {
                MAX_SLOTS = upgradeSetup.capacity;
                inventory.MAX_SLOTS = upgradeSetup.capacity;
            }
            else
            {
                Debug.LogError("UpgradeSetup capacity must be greater than zero.");
            }
        }
        else
        {
            Debug.LogError("UpgradeSetup is not assigned.");

        }
    }

    void OnDestroy()
    {
        inventory.OnInventoryChanged -= UpdateDisplay;
    }

    public void UpdateDisplay()
    {
        List<int> itemsToRemove = new List<int>();

        // Identify items to remove
        foreach (var key in itemsDisplayed.Keys)
        {
            bool found = false;
            foreach (var slot in inventory.Slots)
            {
                if (slot.GetHashCode() == key)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                itemsToRemove.Add(key);
            }
        }

        // Remove old items
        foreach (var key in itemsToRemove)
        {
            ReleaseSlot(key);
        }

        // Display current items in inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory[i].item;
            var slotHash = inventory[i].GetHashCode(); // Use GetHashCode() as a unique key for each slot
            if (!itemsDisplayed.ContainsKey(slotHash))
            {
                CreateDisplay(item, slotHash);
            }
        }
    }

    public void CreateDisplay(ItemObject item, int slotHash)
    {
        if (occupiedPlaces.Count >= MAX_SLOTS)
        {
            Debug.Log("Inventory is full. Cannot add new item: " + item.name);
            return; // Exit the method if the inventory is full
        }

        int i = 0;
        for (; i < NUMBER_OF_COLUMN; i++)
        {
            if (!occupiedPlaces.Contains(i))
            {
                occupiedPlaces.Add(i);
                break;
            }
        }

        var obj = Instantiate(item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = new Vector3(
            X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)),
            0f
        );

        var itemTimer = obj.GetComponent<ItemTimer>();
        var value = obj.GetComponent<ItemValue>();
        if (itemTimer != null)
        {
            itemTimer.OnTimerEnded.AddListener(() => ReleaseSlot(slotHash));
        }
        if (value != null)
        {
            itemTimer.OnTimerEnded.AddListener(() => ReleaseSlot(slotHash));
        }

        itemsDisplayed.Add(slotHash, obj);
        placementMap.Add(obj, i);
    }

    private void ReleaseSlot(int slotHash)
    {
        if (itemsDisplayed.ContainsKey(slotHash))
        {
            var obj = itemsDisplayed[slotHash];
            occupiedPlaces.Remove(placementMap[obj]);
            placementMap.Remove(obj);
            Destroy(obj);
            itemsDisplayed.Remove(slotHash);
            inventory.RemoveItem(slotHash);
        }
    }

    public void RemoveItemByObject(ItemObject item)
    {
        List<int> slotHashesToRemove = new List<int>();
        foreach (var slot in inventory.Slots)
        {
            if (slot.item == item)
            {
                slotHashesToRemove.Add(slot.GetHashCode());
            }
        }

        foreach (var slotHash in slotHashesToRemove)
        {
            ReleaseSlot(slotHash);
        }
    }

}
