using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private SliderController fuelBar;
    [SerializeField] private GameEvent m_LoseEvent;

    [SerializeField] private float maxFuel;

    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelActive = 0.3f;
    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelPassive = 0.03f;

    private float currentFuel;
    public float MaxFuel { get => maxFuel; }
    public float CurrentFuel { get => currentFuel; private set => currentFuel = Mathf.Clamp(value, 0, maxFuel); }

    private float consupDistance = 0;

    private void Start()
    {
        CurrentFuel = MaxFuel;
        fuelBar.SetupBar(CurrentFuel);
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

        if (CurrentFuel == 0)
        {
            m_LoseEvent.Raise();
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
}
