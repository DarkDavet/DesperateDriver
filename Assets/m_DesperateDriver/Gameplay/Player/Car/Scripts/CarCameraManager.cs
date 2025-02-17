using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraManager : MonoBehaviour
{
    public Transform target;
    public float height = 5.0f;
    public float distance = 10.0f;
    public float damping = 0.3f; // Adjust this value to control the damping effect
    public float fixedYPosition;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Calculate the target position
        Vector3 targetPosition = target.position + target.TransformDirection(0f, height, -distance);

        targetPosition.y = fixedYPosition;

        // Smoothly interpolate to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

        // Look at the target
        transform.LookAt(target);
    }
}
