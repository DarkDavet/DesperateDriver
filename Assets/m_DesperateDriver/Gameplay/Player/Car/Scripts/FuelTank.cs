using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private SliderController fuelBar;
    [SerializeField] private GameEvent m_LoseFuelEvent;
    [SerializeField] private UpgradeSetup upgradeSetup;

    [Header("Fuel consumption settings")]
   // [Range(0f, 1f)][SerializeField] private float consuptionFuelActive = 0.3f;
   // [Range(0f, 1f)] [SerializeField] private float consuptionFuelPassive = 0.03f;
    [SerializeField] private float m_DistancePerFullTank = 1000f;

    private bool isFuelDepleted = false;
    private float currentFuel;
    private float maxFuel;
    private float consumptionCoefficient;
    private float consumptionPassiveCoefficient;
    public float MaxFuel { get => maxFuel; private set => maxFuel = Mathf.Clamp(value, 0, 100); }
    public float CurrentFuel { get => currentFuel; private set => currentFuel = Mathf.Clamp(value, 0, maxFuel); }


    public void Init()
    {
        if (upgradeSetup != null && upgradeSetup.capacity > 0)
        {
            consumptionCoefficient = 60f / m_DistancePerFullTank;
            consumptionPassiveCoefficient = consumptionCoefficient / 10;
            MaxFuel = upgradeSetup.capacity;
            CurrentFuel = MaxFuel;
            isFuelDepleted = false;

            fuelBar.SetupBar(MaxFuel);
            fuelBar.UpdateBar(CurrentFuel);
        }
        else
        {
            Debug.LogError("UpgradeSetup is missing or invalid!");
        }
    }

    public float DecreaseFuelLevel(float distance)
    {
        if (isFuelDepleted) return 0;

        float fuelSpent = (distance * consumptionCoefficient) + (Time.deltaTime * consumptionPassiveCoefficient);
        CurrentFuel -= fuelSpent;

        fuelBar.UpdateBar(CurrentFuel);

        if (CurrentFuel <= 0 && !isFuelDepleted)
        {
            CurrentFuel = 0;
            isFuelDepleted = true;
            m_LoseFuelEvent.Raise();
        }

        return CurrentFuel;
    }

    public float IncreaseFuelLevel(float fuelAmount)
    {
        if (fuelAmount > 0)
        {
            CurrentFuel += fuelAmount;
            fuelBar.UpdateBar(CurrentFuel);
        }
        return CurrentFuel;
    }

    public void Upgrade(int capacity)
    {
        MaxFuel = capacity;
    }
}
