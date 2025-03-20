using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private SliderController fuelBar;
    [SerializeField] private GameEvent m_LoseFuelEvent;
    [SerializeField] private UpgradeSetup upgradeSetup;

    

    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelActive = 0.3f;
    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelPassive = 0.03f;

    private float currentFuel;
    private float maxFuel;
    public float MaxFuel { get => maxFuel; private set => maxFuel = Mathf.Clamp(value, 0, 100); }
    public float CurrentFuel { get => currentFuel; private set => currentFuel = Mathf.Clamp(value, 0, maxFuel); }

    private float consupDistance = 0;
    private bool isFuelDepleted = false;

    private void Start()
    {
        if (upgradeSetup != null)
        {
            if (upgradeSetup.capacity > 0)
            {
                maxFuel = upgradeSetup.capacity;
                CurrentFuel = maxFuel;
                fuelBar.SetupBar(CurrentFuel);
            }
            else
            {
                Debug.LogError("UpgradeSetup capacity must be greater than zero.");
            }
        }
        else
        {
            Debug.LogError("UpgradeSetup is not assigned.");

        }
    }

    public float DecreaseFuelLevel(float distance)
    {
        consupDistance += distance;
        if (consupDistance >= 0.05)
        {
            CurrentFuel -= consuptionFuelActive;
            consupDistance = 0;
            fuelBar.UpdateBar(CurrentFuel);
        }

        if (CurrentFuel == 0 && !isFuelDepleted)
        {
            isFuelDepleted = true; // Prevent further event raising
            m_LoseFuelEvent.Raise();
        }
        //CurrentFuel -= consuptionFuelPassive * Time.deltaTime; 
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
