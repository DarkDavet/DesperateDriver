using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSetup", menuName = "Player Setups/UpgradeSetup")]
public class UpgradeSetup : ScriptableObject
{
    public int level;
    public int capacity;
    public int capacityNext;
    public int cost;
}
