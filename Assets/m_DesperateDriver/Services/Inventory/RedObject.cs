using UnityEngine;

[CreateAssetMenu(fileName = "New Red Object", menuName = "Inventory System/Items/Red")]
public class RedObject: ItemObject
{
    private void Awake()
    {
        type = ItemCategory.Red;
    }
}
