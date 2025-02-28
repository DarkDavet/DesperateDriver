using UnityEngine;

public class IceCreamStation : MonoBehaviour
{
    [SerializeField] private Item iceCream;
    [SerializeField] private InventoryDisplay display;

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
        display.RemoveItemByObject(iceCream.item);
    }
}
