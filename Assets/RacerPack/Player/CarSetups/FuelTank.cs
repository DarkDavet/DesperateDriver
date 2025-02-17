using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] private SliderController fuelBar;

    [SerializeField] private float maxFuel;

    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelActive = 0.3f;
    [Range(0f, 1f)]
    [SerializeField] private float consuptionFuelPassive = 0.03f;

    private float currentFuel;

    private float kilometer = 0;

    private void Start()
    {
        currentFuel = maxFuel;
        fuelBar.SetupBar(currentFuel);
    }

    public float DecreaseFuelLevel(float distance)
    {
        kilometer += distance;
        if (kilometer >= 1)
        {
            currentFuel -= consuptionFuelActive;
            fuelBar.UpdateBar(currentFuel);
            kilometer = 0;
        }
        currentFuel -= consuptionFuelPassive * Time.deltaTime;

        return currentFuel;
    }
}
