using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemCategory
{
    Red,
    Yellow,
    Green
}

public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemCategory type;
    public int expiryTime;

    public virtual void Use()
    {

    }
}