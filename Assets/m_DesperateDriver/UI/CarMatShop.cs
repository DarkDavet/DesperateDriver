using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarMatShop : SingletonLocal<CarMatShop>
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private List<MatProduct> matGoods;
    [SerializeField] private PlayerSetup playerSetup;
    [SerializeField] private Renderer carBodyAppearance;
    [SerializeField] private Renderer carSeamsAppearance;
    [SerializeField] private UIDialogWindow dialogWindow;
    [SerializeField] private UIMonologWindow monologWindow;

    private string matNameCurrent;
    private MatProduct activeProduct;

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
            product.OnProductActivated += SetDesireMaterial;
        }
    }

    public void OnPurchaseButtonClicked()
    {
        if (activeProduct != null)
        {
            if (!activeProduct.IsPurchased)
            {
                TryPurchase();
            }
            else
            {
                PaintCar(activeProduct);
            }
        }    
    }

    public void SetDesireMaterial(MatProduct activeProduct)
    {
        this.activeProduct = activeProduct;
    }

    private void TryPurchase()
    {
        if (gameInventory.RequestPayment(activeProduct.Price, ItemType.MONEY))
        {
            dialogWindow.SetupWindow(
            $"Are you sure you want to unlock {activeProduct.MatTitle} for {activeProduct.Price}$?",
            () => Purchase(), // Confirm action
            () => Debug.Log("Purchase cancelled.") // Cancel action
        );
        }
        else
        {
            monologWindow.SetupWindow(
            $"You haven't enough money to unlock {activeProduct.MatTitle}",
            "OK",
            () => Debug.Log("Purchase cancelled.")
            );
        }
    }

    private void Purchase()
    {
        gameInventory.ProcessPayment(activeProduct.Price, ItemType.MONEY);
        activeProduct.UnlockConcreteProduct();
        StorageManager.Instance.SaveCarColorsData(matGoods);
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

