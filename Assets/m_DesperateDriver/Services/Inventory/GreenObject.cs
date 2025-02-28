using UnityEngine;

[CreateAssetMenu(fileName = "New Green Object", menuName = "Inventory System/Items/Green")]
public class GreenObject: ItemObject
{
    private void Awake()
    {
        type = ItemCategory.Green;
    }
}
