using System;
using UnityEngine;

public class IceCreamStation : MonoBehaviour
{
    [SerializeField] private Item iceCream;
    [SerializeField] private InventoryDisplay display;

    //public event Action OnSoldItem;

    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            SaleItems(other);
        }
    }

    public void SaleItems(Collider other)
    {
        // OnSoldItem?.Invoke(iceCream.item.type);
        display.RemoveItemByObject(iceCream.item);
    }
}
