using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CarInputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightManager))]
public class VehicleController : MonoBehaviour
{
    [SerializeField] private CarInputManager inputManager;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private UIManager uIManager;
    public List<WheelCollider> thortleWheels;
    public List<WheelCollider> steerWheels;
    public List<GameObject> brakeLamps; 
    public List<GameObject> meshes;
    public float strengthCofficient = 20000f;
    public float maxTurn = 20f;
    public float brakeStrength;

    public Rigidbody rb;
    public Transform centerOfMass;

    private void Start()
    {
        Application.targetFrameRate = 60; // Set to your desired frame rate
        QualitySettings.vSyncCount = 1; // Enable VSync
        inputManager = GetComponent<CarInputManager>();
        lightManager = GetComponent<LightManager>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (centerOfMass)
        {
            rb.centerOfMass = centerOfMass.localPosition;
        }
    }

    private void Update()
    {
        if (inputManager.lit)
        {
            lightManager.SwitchLights();
        }

        foreach (GameObject lamp in brakeLamps)
        {
            lamp.GetComponent<Renderer>().material.SetColor("_Color", inputManager.brake ? new Color(0.5f, 0.11f, 0.11f) : Color.black);
        }
        if (inputManager.brake)
        {
            lightManager.SwitchBrakeLights(true);
        }
        else
        {
            lightManager.SwitchBrakeLights(false);
        }
        //uIManager.ChangeSpeed(transform.InverseTransformVector(rb.velocity).z);
    }


    private void FixedUpdate()
    {
        foreach (WheelCollider wheel in thortleWheels)
        {
            if (inputManager.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength * Time.deltaTime * 5;
                Debug.Log("Brakes work");
            }
            else
            {
                wheel.motorTorque = strengthCofficient * Time.deltaTime * inputManager.thortle;
                wheel.brakeTorque = 0f;
            }
           
        }

        steerWheels[0].GetComponent<WheelCollider>().steerAngle = maxTurn * inputManager.steer;
        steerWheels[0].transform.localEulerAngles = new Vector3(0f, inputManager.steer * maxTurn, -180f);
        steerWheels[1].GetComponent<WheelCollider>().steerAngle = maxTurn * inputManager.steer;
        steerWheels[1].transform.localEulerAngles = new Vector3(0f, inputManager.steer * maxTurn, 0f);

        foreach (GameObject mech in meshes)
        {
            mech.transform.Rotate(rb.linearVelocity.magnitude * (transform.InverseTransformDirection(rb.linearVelocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.31f), 0f, 0f);
        }
    }
}
