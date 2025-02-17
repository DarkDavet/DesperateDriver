using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public List<Light> lights;
    public List<Light> brakeLights;

    private void Start()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    public void SwitchLights()
    {
        foreach (Light light in lights)
        {
            if (light.enabled)
            {
                light.enabled = false;
                AudioManager.Instance.Play("LightsOFF");
            }
            else
            {
                light.enabled = true;
                AudioManager.Instance.Play("LightsON");
            }
        }
    }

    public void SwitchBrakeLights(bool lightsOn)
    {
        foreach (Light light in brakeLights)
        {
            if (lightsOn)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }
}
