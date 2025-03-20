using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TransactionService : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private List<Product> goods;
    [SerializeField] private UIDialogWindow dialogWindow;

    private Product purchasedProduct;

    public void InitService()
    {
        StorageManager.Instance.LoadGoodsData(goods);
        foreach (var item in goods)
        {
            if (item.isUnlock == false)
            {
                item.LockProducts();
                item.isActive = false;
                item.icon.color = Color.red;
                ShowCosts(item);
            }
            if (item.isUnlock == true)
            {
                item.GetPurchased();
            }
        }
    }

    private void ShowCosts(Product product)
    {
        product.costTitle.text = product.cost.ToString();
        if (product.cost == 0 )
        {
            product.costTitle.text = "";
        }
    }

    public void OnPurchaseButtonClicked()
    {
        if (SearchDesireItem())
        {
            // Define actions for confirmation
            dialogWindow.SetupWindow(
                $"Are you sure you want to unlock {purchasedProduct.productName} for {purchasedProduct.cost}$?",
                () => Purchase(), // Confirm action
                () => Debug.Log("Purchase cancelled.") // Cancel action
            );
        }
    }

    public void Purchase()
    {
        if (PurchaseProcess())
        {
            purchasedProduct.isUnlock = true;
            purchasedProduct.GetPurchased();
            StorageManager.Instance.SaveGoodsData(goods);
        }   
    }

    public bool SearchDesireItem()
    {
        foreach (var item in goods)
        {
            if (item.isActive)
            {
                purchasedProduct = item;
                return true;
            }
        }
        return false;
    }

    private bool PurchaseProcess()
    {
        if (purchasedProduct != null && gameInventory.RequestPayment(purchasedProduct.cost, ItemType.KEY))
        {
            return true;
        }
        return false;
    }
}
