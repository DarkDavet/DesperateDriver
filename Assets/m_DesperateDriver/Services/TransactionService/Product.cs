using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Product : MonoBehaviour
{
    public string productName;
    public int cost;
    public Button unlockButton;
    public Image icon;
    public TextMeshProUGUI costTitle;
    public List<Button> dependableButtons;
    public List<Level> unlockableLevels;
    public bool isUnlock;
    [NonSerialized] public bool isActive = false;

    public void OnButtonClick()
    {
        isActive = true;
    }

    public void GetPurchased()
    {
        isActive = false;
        cost = 0;
        unlockButton.interactable = false;
        icon.color = Color.green;
        costTitle.text = string.Empty;
        foreach (var button in dependableButtons)
        {
            button.interactable = true;
            Color newColor;

            if (UnityEngine.ColorUtility.TryParseHtmlString("#AFF2A5", out newColor))
            {
                button.image.color = newColor;
            }
        }

        foreach(var level in unlockableLevels)
        {
            level.isPlayable = true;
        }
    }

    public void LockProducts()
    {
        foreach (var level in unlockableLevels)
        {
            level.isPlayable = false;
        }
    }

    public ProductData PackProductData()
    {
        var data = new ProductData
        {
            isUnlock = this.isUnlock,
        };
        return data;
    }

    public void UnpackProductData(ProductData data)
    {
        this.isUnlock = data.isUnlock;
    } 
}
public class ProductData
{
    public bool isUnlock;
}
