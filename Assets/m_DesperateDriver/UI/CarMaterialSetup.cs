using UnityEngine;

[CreateAssetMenu(fileName = "CarMaterialSetup", menuName = "Car Material's setups/Car Material's setup")]
public class CarMaterialSetup : ScriptableObject
{
    public string title;
    public Material bodyMat;
    public Material seamsMat;
    public int price;
    public Color iconColor;
}
