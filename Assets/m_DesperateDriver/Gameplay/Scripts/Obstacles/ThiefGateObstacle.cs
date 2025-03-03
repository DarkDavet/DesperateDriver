using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class ThiefGateObstacle : MonoBehaviour
{
    [SerializeField] private List<ItemObject> allItems = new List<ItemObject>();
    [SerializeField] private InventoryObject inventoryObj;
    [SerializeField] private InventoryDisplay inventoryDisplay;
    private List<ItemObject> hasItems = new List<ItemObject>();

    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(playerTag))
        {
            if (CheckAvailableItems())
            {
                inventoryDisplay.RemoveItemByObject(GetRandomItem());
            }
        }   
    }

    private bool CheckAvailableItems()
    {
        hasItems.Clear();
        foreach(var item in allItems)
        {
            if (inventoryObj.Contains(item))
            {
                hasItems.Add(item);
            }
        }

        if (hasItems.Count > 0)
        {
            return true;
        }
        return false;
    }

    private ItemObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, hasItems.Count);
        return hasItems[randomIndex];
    }
}
