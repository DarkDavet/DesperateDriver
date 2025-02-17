using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    public float thortle;
    public float steer;
    [HideInInspector] public bool lit;
    [HideInInspector] public bool brake;
    void Update()
    {
        thortle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        lit = Input.GetKeyDown(KeyCode.L);
        brake = Input.GetKey(KeyCode.Space);
    }
}
