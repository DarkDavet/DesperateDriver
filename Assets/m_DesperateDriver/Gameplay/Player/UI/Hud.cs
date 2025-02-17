using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI crystalText;

    private int m_CrystalValue;
    public int CrystalValue
    {
        get => m_CrystalValue;
        set
        {
            if (m_CrystalValue != value)
            {
                m_CrystalValue = value;
                if (crystalText != null)
                {
                    crystalText.text = CrystalValue.ToString();
                }
                else
                {
                    Debug.LogError("crystalText is not assigned!");
                }
            }
        }
    }
}
