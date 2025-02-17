using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour
{
    public WheelController wheelController;
    public VehicelAccessoryController accessoryController;
    public Speedometer speedometer;
    public Gear[] gears;

    private int currentGear = 0;
    private float currentSpeed = 0;

    private float minGearSpeed = 0;
    private float maxGearSpeed = 0;


    public void SetupGear()
    {
        currentSpeed = accessoryController.speedInfo;
        foreach (Gear gear in gears)
        {
            if (currentSpeed < gear.maxSpeedLimit && currentSpeed >= gear.minSpeedLimit)
            {
                currentGear = gear.number;
                minGearSpeed = gear.minSpeedLimit;
                maxGearSpeed = gear.maxSpeedLimit;
                wheelController.maxTorque = gear.torque;
                break;
            }
        }
    }

    public void UpdateGears()
    {
        currentSpeed = accessoryController.speedInfo;
        if (currentSpeed > maxGearSpeed && currentSpeed < minGearSpeed)
        {
            SwitchGears();
        }
        //Debug.Log($"speed: {VehicelAccessoryController.Instance.speedInfo}; torque: {wheelController.torque}");
        Debug.Log($"gear^ {currentGear}");
    }

    public void SwitchGears()
    {
        foreach (Gear gear in gears)
        {
            if (currentSpeed < gear.maxSpeedLimit && currentSpeed >= gear.minSpeedLimit && currentGear != gear.number)
            {
                currentGear = gear.number;
                minGearSpeed = gear.minSpeedLimit;
                maxGearSpeed = gear.maxSpeedLimit;
                wheelController.maxTorque = gear.torque;
                break;
            }
        }
    }
}
