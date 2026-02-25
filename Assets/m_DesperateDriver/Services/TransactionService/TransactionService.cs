using System.Collections.Generic;
using UnityEngine;

public class TransactionService : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private List<Product> goods;
    [SerializeField] private UIDialogWindow dialogWindow;
    [SerializeField] private UIMonologWindow monologWindow;

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
                item.icon.color = Color.grey;
            }
        }
    }

    private void ShowCosts(Product product)
    {
        product.costTitle.text = product.cost.ToString();
        if (product.cost == 0)
        {
            product.costTitle.text = "";
        }
    }

    public void OnPurchaseButtonClicked()
    {

        if (SearchDesireItem())
        {
            TryPurchase();
        }

        ResetTransaction();
    }

    private void ResetTransaction()
    {
        foreach (var item in goods)
        {
            item.isActive = false;
        }
    }

    public void Purchase()
    {
        gameInventory.ProcessPayment(purchasedProduct.cost, ItemType.KEY);
        purchasedProduct.isUnlock = true;
        purchasedProduct.GetPurchased();            
        StorageManager.Instance.SaveGoodsData(goods);
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

    private void TryPurchase()
    {
        if (gameInventory.RequestPayment(purchasedProduct.cost, ItemType.KEY))
        {
            // Define actions for confirmation
            dialogWindow.SetupWindow(
                $"Are you sure you want to unlock {purchasedProduct.productName} for {purchasedProduct.cost} stars?",
                () => Purchase(), // Confirm action
                () => Debug.Log("The transaction is canceled.") // Cancel action
            );
        }
        else
        {
           monologWindow.SetupWindow(
           $"You haven't enough stars to unlock {purchasedProduct.productName}",
           "OK",
           () => Debug.Log("The transaction is canceled.")
           );
        }
    }
}
