using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TransactionService : MonoBehaviour
{
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private List<Product> goods;

    private Product purchasedProduct;

    private void Start()
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

    public void Purchase()
    {
        if (SearchDesireItem())
        {
            if (PurchaseProcess())
            {
                purchasedProduct.isUnlock = true;
                purchasedProduct.GetPurchased();
                StorageManager.Instance.SaveGoodsData(goods);
            }
        };      
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
        if (purchasedProduct != null && gameInventory.RequestPayment(purchasedProduct.cost))
        {
            return true;
        }
        return false;
    }
}
