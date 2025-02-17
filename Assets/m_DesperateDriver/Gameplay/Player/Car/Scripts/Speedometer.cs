using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private SliderController speedBar;
    [SerializeField] private float maxSpeed;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = maxSpeed;
        speedBar.SetupBar(currentSpeed);
        
    }
    public float CalculateSpeed(float speed)
    {
        currentSpeed = speed * 3.6f;
        speedBar.UpdateBar(currentSpeed);
        return currentSpeed;
    }
}
