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
    public float CurrentSpeed { get => currentSpeed; private set { currentSpeed = value; } }

    public void Init()
    {
        CurrentSpeed = maxSpeed;
        speedBar.SetupBar(CurrentSpeed);
        
    }
    public float CalculateSpeed(float speed)
    {
        CurrentSpeed = speed * 3.6f;
        speedBar.UpdateBar(CurrentSpeed);
        return CurrentSpeed;
    }
}
