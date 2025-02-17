using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController: MonoBehaviour
{
    public Image[] targetBars;

    private float amountMax;

    //private Text amountText;
    // Update is called once per frame
    public void SetupBar(float amountMax)
    {
        this.amountMax = amountMax;
        foreach (var bar in targetBars)
        {
            bar.fillAmount = 0;
        }

        if (amountMax <= 0)
        {
            Debug.LogError("amountMax must be greater than zero.");
        }
    }

    public void UpdateBar(float amount)
    {
        if (amountMax > 0)
        { 
            float fillAmount = amount / amountMax;
            
            if (float.IsNaN(fillAmount) || float.IsInfinity(fillAmount))
            {
                Debug.LogError("Calculated fillAmount is invalid: " + fillAmount);
            }
            else
            {
                foreach (var bar in targetBars)
                {
                    bar.fillAmount = fillAmount;
                }
            }
        }
        else
        {
            Debug.LogError("amountMax is not set or is zero.");
        }
    }
}
