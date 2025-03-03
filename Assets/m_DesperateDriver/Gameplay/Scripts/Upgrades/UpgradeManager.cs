using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private Dictionary<int, float> fuelCapacityUpgrades = new Dictionary<int, float>
    {
        { 1, 1.2f },
        { 2, 1.25f },
        { 3, 1.15f }
    };

    private int currentUpgradeLevel = 0;
    private float baseFuelCapacity = 100f; // Базовая вместимость топлива
    private float currentFuelCapacity;

    void Start()
    {
        currentFuelCapacity = baseFuelCapacity;
    }

    // Метод для покупки апгрейда
    public void PurchaseUpgrade()
    {
        currentUpgradeLevel++;
        if (fuelCapacityUpgrades.ContainsKey(currentUpgradeLevel))
        {
            currentFuelCapacity *= fuelCapacityUpgrades[currentUpgradeLevel];
            Debug.Log("Новая вместимость топлива: " + currentFuelCapacity);
        }
        else
        {
            Debug.Log("Нет доступных апгрейдов для текущего уровня.");
        }
    }
}
