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

    public void PurchaseUpgrade()
    {
        int tmpCurrentUpgradeLevel = currentUpgradeLevel + 1;
        if (upgradeDictionary.FuelCapacityUpgrades.ContainsKey(tmpCurrentUpgradeLevel) && upgradeDictionary.FuelUpgradesCosts.ContainsKey(tmpCurrentUpgradeLevel))
        {
            if (gameInventory.RequestPayment(upgradeDictionary.FuelUpgradesCosts[tmpCurrentUpgradeLevel], ItemType.MONEY))
            {
                currentUpgradeLevel = tmpCurrentUpgradeLevel;
                SetUpgradeSetup();
                upgradeData.SaveUpgradeData();
            }
            
        }
        else
        {
            Debug.Log("No available upgrades for current level.");
        }
    }

    public void GetUpgradelevelFromData(Dictionary<string, int> levelData)
    {
        currentUpgradeLevel = levelData[title];
        SetUpgradeSetup();
    }
}
