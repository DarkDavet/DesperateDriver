using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicelAccessoryController : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    [SerializeField] private WheelController wheelController;
    [SerializeField] private Speedometer speedometer;
    [SerializeField] private Tach tach;
    [SerializeField] private FuelTank fuelTank;
    [SerializeField] private Transmission transmission;

    [SerializeField] private UIManager uiManager;
    public List<GameObject> brakeLamps;
    public Color brakeColor;

    private KeyCode switchFrontLight = KeyCode.L;
    private KeyCode hitKlaxon = KeyCode.K;

    private Color originalColorBrakeLamps;

    private Renderer[] renderersBrakeLamps;

    private Rigidbody rb;
    [NonSerialized]
    public float speedInfo;
    private float speedTech;
    private float distanceTotalInfo;
    private float distancePerFrameInfo;
    private float fuelInfo;
 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderersBrakeLamps = new Renderer[brakeLamps.Count];
        for (int i = 0; i < brakeLamps.Count; i++)
        {
            originalColorBrakeLamps = brakeLamps[i].GetComponent<Renderer>().material.color;
            renderersBrakeLamps[i] = brakeLamps[i].GetComponent<Renderer>();
        }

        transmission.SetupGear();
    }

    private void Update()
    {
        MechanicAccessoryUpdate();
        ElectroAccessoryUpdate();
    }

    private void MechanicAccessoryUpdate()
    {
        speedTech = Mathf.Abs(transform.InverseTransformVector(rb.linearVelocity).z);
        speedInfo = speedometer.CalculateSpeed(speedTech);
        distancePerFrameInfo = tach.ShowDistancePerFrame();
        distanceTotalInfo = tach.CalculateDistance(speedInfo);

        transmission.UpdateGears();

        uiManager.ShowSpeed(speedInfo);
        uiManager.ShowDistance(distanceTotalInfo);
    }

    private void ElectroAccessoryUpdate()
    {
        fuelInfo = fuelTank.DecreaseFuelLevel(distancePerFrameInfo); 
        transmission.SwitchGears();

        if (Input.GetKeyDown(switchFrontLight))
        {
            lightManager.SwitchLights();
        }

        foreach (Renderer lampRenderer in renderersBrakeLamps)
        {
            //lampRenderer.material.SetColor("_EmissionColor", Input.GetKey(driveManager.pressBrakes) ? brakeColor : originalColorBrakeLamps);

            if (Input.GetKey(wheelController.pressBrakes))
            {
                lampRenderer.material.EnableKeyword("_EMISSION");
                lampRenderer.material.SetColor("_EmissionColor", brakeColor);
            }
            else
            {
                lampRenderer.material.SetColor("_EmissionColor", originalColorBrakeLamps);
                lampRenderer.material.DisableKeyword("_EMISSION");
            }
        }

        if (Input.GetKey(wheelController.pressBrakes))
        {
            lightManager.SwitchBrakeLights(true);
        }
        else
        {
            lightManager.SwitchBrakeLights(false);
        }
        
        if (Input.GetKeyDown(hitKlaxon))
        {
            AudioManager.Instance.Play("Klaxon");
        }
        
    }
}
