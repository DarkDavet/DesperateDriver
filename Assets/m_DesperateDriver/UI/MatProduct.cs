using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatProduct : MonoBehaviour
{
    [SerializeField] private CarMaterialSetup matSetup;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI priceTitle;
    [SerializeField] private Image icon;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Button button;

    public event Action<MatProduct> OnProductActivated;
    private bool isPurchased = false;
    private int price;
    private string matTitle;
    public bool IsPurchased { get { return isPurchased; } private set { isPurchased = value; } }
    public int Price { get { return price; } private set { price = value; } }
    public string MatTitle { get { return matTitle; } private set { matTitle = value; } }

    public void OnButtonClick()
    {
        OnProductActivated?.Invoke(this);
    }

    public void InitConcreteProduct()
    {
        MatTitle = matSetup.title;
        title.text = matSetup.title;
        icon.color = matSetup.iconColor;
        if (IsPurchased || matSetup.price == 0)
        {
            Debug.Log($"Unlock mat {matSetup.name} is initialized");
            lockIcon.gameObject.SetActive(false);
            priceTitle.gameObject.SetActive(false);
        }
        else if (!IsPurchased)
        {
            Debug.Log($"Lock mat {matSetup.name} is initialized");
            priceTitle.text = matSetup.price.ToString();
            Price = matSetup.price;
        }
    }

    public void UnlockConcreteProduct()
    {
        Debug.Log($"{matSetup.name} is unlocked");
        IsPurchased = true;
        lockIcon.gameObject.SetActive(false);
        priceTitle.gameObject.SetActive(false);
    }

    public Material ExtractBodyMaterial()
    {
        if (IsPurchased)
        {
            return matSetup.bodyMat;
        }
        return null;
    }

    public Material ExtractSeamsMaterial()
    {
        if (IsPurchased)
        {
            return matSetup.seamsMat;
        }
        return null;
    }

    public MatProductData PackProductData()
    {
        var data = new MatProductData
        {
            isPurchased = this.IsPurchased
        };
        return data;
    }

    public void UnpackProductData(MatProductData data)
    {
        this.IsPurchased = data.isPurchased;
    }
}
public class MatProductData
{
    public bool isPurchased;
}

