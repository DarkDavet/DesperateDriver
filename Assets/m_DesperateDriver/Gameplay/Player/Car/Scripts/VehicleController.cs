using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public List<GameObject> brakeLamps;
    public Color brakeColor;

    private LightManager lightManager;
    private WheelController wheelController;
    private Speedometer speedometer;
    private Tach tach;
    private FuelTank fuelTank;
    private Transmission transmission;
    private Rigidbody rb;

    private KeyCode switchFrontLight = KeyCode.L;
    private KeyCode hitKlaxon = KeyCode.K;

    private Color originalColorBrakeLamps;

    private Renderer[] renderersBrakeLamps;

    [NonSerialized]
    public float speedInfo;
    private float speedTech;
    private float distanceTotalInfo;
    private float distancePerFrameInfo;

    private bool isInit = false;

    private void Awake()
    {
        lightManager = GetComponent<LightManager>();
        wheelController = GetComponent<WheelController>();
        speedometer = GetComponent<Speedometer>();
        tach = GetComponent<Tach>();
        fuelTank = GetComponent<FuelTank>();
        transmission = GetComponent<Transmission>();
        rb = GetComponent<Rigidbody>();
        
    }
    public void Init()
    {
        renderersBrakeLamps = new Renderer[brakeLamps.Count];
        for (int i = 0; i < brakeLamps.Count; i++)
        {
            originalColorBrakeLamps = brakeLamps[i].GetComponent<Renderer>().material.color;
            renderersBrakeLamps[i] = brakeLamps[i].GetComponent<Renderer>();
        }

        wheelController.Init();
        lightManager.Init();
        transmission.SetupGear();
        fuelTank.Init();
        speedometer.Init();
        
        isInit = true;
    }

    private void Update()
    {
        if (!isInit) return;

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
        fuelTank.DecreaseFuelLevel(distancePerFrameInfo); 
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
