using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceText;

    public void ShowSpeed(float speed)
    {
        speedText.text = Mathf.Clamp(Mathf.Round(speed), 0f, 1000f) + "";
    }

    public void ShowDistance(float distance)
    {
        distanceText.text = Mathf.Clamp(Mathf.Round(distance), 0f, 100000f) + " km";
    }
}
