using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tach : MonoBehaviour
{
    private float distanceTotal;

    private float distancePerFrame;

    public float CalculateDistance(float speed)
    {
        distancePerFrame = (speed * Time.deltaTime) / 1000;

        distanceTotal += distancePerFrame;

        return distanceTotal;
    }

    public float ShowDistancePerFrame()
    {
        return distancePerFrame;
    }
}
