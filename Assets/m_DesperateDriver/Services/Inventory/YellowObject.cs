using UnityEngine;

[CreateAssetMenu(fileName = "New Yellow Object", menuName = "Inventory System/Items/Yellow")]
public class YellowObject: ItemObject
{
    private void Awake()
    {
        type = ItemCategory.Yellow;
    }
}
