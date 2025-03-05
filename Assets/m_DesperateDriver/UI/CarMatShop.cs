using System;
using System.Collections.Generic;
using UnityEngine;

public class CarMatShop : SingletonLocal<CarMatShop>
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private List<MatProduct> matGoods;
    [SerializeField] private PlayerSetup playerSetup;
    [SerializeField] private Renderer carBodyAppearance;
    [SerializeField] private Renderer carSeamsAppearance;

    private string matNameCurrent;

    public void InitShop()
    {
        StorageManager.Instance.LoadCarColorsData(matGoods);
        StorageManager.Instance.LoadCarMaterialData(Instance);
        Debug.Log($"Mat title current: {matNameCurrent}");
        foreach (MatProduct product in matGoods)
        {
            product.InitConcreteProduct();
            if (product.MatTitle == matNameCurrent)
            {
                Debug.Log($"Mat title matched: {product.MatTitle}");
                PaintCar(product);
            }
            product.OnProductActivated += ShopProcessing;
        }
    }

    public void ShopProcessing(MatProduct activeProduct)
    {
        if (activeProduct != null)
        {
            if (!activeProduct.IsPurchased && gameInventory.RequestPayment(activeProduct.Price))
            {
                activeProduct.UnlockConcreteProduct();
                StorageManager.Instance.SaveCarColorsData(matGoods);
            }
            else if (activeProduct.IsPurchased)
            {
                PaintCar(activeProduct);
            }
        }
    }

    private void PaintCar(MatProduct activeProduct)
    {
        playerSetup.carBodyMaterial = activeProduct.ExtractBodyMaterial();
        playerSetup.carSeamsMaterial = activeProduct.ExtractSeamsMaterial();
        carBodyAppearance.material = activeProduct.ExtractBodyMaterial();
        carSeamsAppearance.material = activeProduct.ExtractSeamsMaterial();
        matNameCurrent = activeProduct.MatTitle;
        StorageManager.Instance.SaveCarMaterialData(Instance);
        Debug.Log("Material has aplied");
    }

    public CarMaterialData PackCarMaterialData()
    {
        var data = new CarMaterialData
        {
            matTitle = this.matNameCurrent
        };
        return data;
    }

    public void UnpackCarMaterialData(CarMaterialData data)
    {
        this.matNameCurrent = data.matTitle;
    }
}
public class CarMaterialData
{
    public string matTitle;
}

