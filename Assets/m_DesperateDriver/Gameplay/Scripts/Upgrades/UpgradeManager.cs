using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] private UpgradeSetup upgradeSetup;
    [SerializeField] private UpgradeSetup upgradeSetupForWidget;
    [SerializeField] private UpgradeDictionary upgradeDictionary;
    [SerializeField] private UpgradeData upgradeData;
    [SerializeField] private GameInventory gameInventory;
    [SerializeField] private UIUpgradeWidget upgradeWidget;
    [SerializeField] private UIDialogWindow dialogWindow;
    [SerializeField] private UIMonologWindow monologWindow;


    private int currentUpgradeLevel = 0;

    public void InitManager()
    {
        upgradeDictionary.InitDictionary();
        upgradeData.InitDictionary(title, currentUpgradeLevel);
        SetUpgradeSetup();
    }
    private void SetUpgradeSetup()
    {
        upgradeSetup.level = currentUpgradeLevel;
        upgradeSetup.capacity = upgradeDictionary.FuelCapacityUpgrades[currentUpgradeLevel];
        upgradeSetup.cost = upgradeDictionary.FuelUpgradesCosts[currentUpgradeLevel];

        upgradeData.UpdateDictionary(title, currentUpgradeLevel);
        PackUpgradeSetupForWidget();
    }

    private void PackUpgradeSetupForWidget()
    {
        upgradeSetupForWidget.level = currentUpgradeLevel;
        upgradeSetupForWidget.capacity = upgradeDictionary.FuelCapacityUpgrades[currentUpgradeLevel];

        int tmpCurrentUpgradeLevel = currentUpgradeLevel + 1;
        if (upgradeDictionary.FuelCapacityUpgrades.ContainsKey(tmpCurrentUpgradeLevel))
        {
            upgradeSetupForWidget.capacityNext = upgradeDictionary.FuelCapacityUpgrades[currentUpgradeLevel + 1];
            upgradeSetupForWidget.cost = upgradeDictionary.FuelUpgradesCosts[currentUpgradeLevel + 1];
        }
        else
        {
            upgradeSetupForWidget.capacityNext = -1;
            upgradeSetupForWidget.cost = -1;
        }
        upgradeWidget.SetupWidget(upgradeSetupForWidget);
    }
    public void OnPurchaseButtonClicked()
    {
        int tmpCurrentUpgradeLevel = currentUpgradeLevel + 1;
        if (upgradeDictionary.FuelCapacityUpgrades.ContainsKey(tmpCurrentUpgradeLevel) && upgradeDictionary.FuelUpgradesCosts.ContainsKey(tmpCurrentUpgradeLevel))
        {
            TryPurchase(tmpCurrentUpgradeLevel);
        }
        else
        {
            Debug.Log("No available upgrades for current level.");
        }
    }

    private void TryPurchase(int tmpCurrentUpgradeLevel)
    {
        if (gameInventory.RequestPayment(upgradeDictionary.FuelUpgradesCosts[tmpCurrentUpgradeLevel], ItemType.MONEY))
        {
            dialogWindow.SetupWindow(
            $"Are you sure you want to upgrade {title} for {upgradeDictionary.FuelUpgradesCosts[tmpCurrentUpgradeLevel]}$?",
            () => Purchase(tmpCurrentUpgradeLevel), // Confirm action
            () => Debug.Log("Transaction canceled.") // Cancel action
        );
        }
        else
        {
            monologWindow.SetupWindow(
            $"You haven't enough money to upgrade {title}",
            "OK",
            () => Debug.Log("Transaction canceled.")
            );
        }
    }

    public void Purchase(int tmpCurrentUpgradeLevel)
    {
        gameInventory.ProcessPayment(upgradeDictionary.FuelUpgradesCosts[tmpCurrentUpgradeLevel], ItemType.MONEY);
        currentUpgradeLevel = tmpCurrentUpgradeLevel;
        SetUpgradeSetup();
        upgradeData.SaveUpgradeData();
    }

    public void GetUpgradelevelFromData(Dictionary<string, int> levelData)
    {
        currentUpgradeLevel = levelData[title];
        SetUpgradeSetup();
    }
}
