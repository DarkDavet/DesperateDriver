using UnityEngine;

public class IceCreamTrunk: MonoBehaviour
{
    [SerializeField] private InventoryObject trunk;
    private const string itemTag = "IceCream";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            TakeItem(other);
        }        
    }

    public void TakeItem(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            if (trunk.AddItem(item.item))
            {
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
    }

    private void OnApplicationQuit()
    {
        trunk.Clear();
    }
}
