using UnityEngine;

[CreateAssetMenu(fileName = "CarMaterialSetup", menuName = "Car Material's setups/Car Material's setup")]
public class CarMaterialSetup : ScriptableObject
{
    public string title;
    public Material material;
    public bool isUnlock;
    public string price;
    public Color iconColor;
}
