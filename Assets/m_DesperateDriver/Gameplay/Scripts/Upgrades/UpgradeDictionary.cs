using System.Collections.Generic;
using UnityEngine;

public class UpgradeDictionary: MonoBehaviour
{
    [SerializeField] private DictionarySetup dictionaryCapacitySetup;
    [SerializeField] private DictionarySetup dictionaryCostSetup;

    public Dictionary<int, int> FuelCapacityUpgrades { get; private set; } = new Dictionary<int, int>
    {
        { 0, 0 },
        { 1, 0 },
        { 2, 0 },
        { 3, 0 }
    };
    public Dictionary<int, int> FuelUpgradesCosts { get; private set; } = new Dictionary<int, int>
    {
        { 0, 0 },
        { 1, 0 },
        { 2, 0 },
        { 3, 0 }
    };

    public void InitDictionary()
    {
        FuelCapacityUpgrades[0] = dictionaryCapacitySetup.value1;
        FuelCapacityUpgrades[1] = dictionaryCapacitySetup.value2;
        FuelCapacityUpgrades[2] = dictionaryCapacitySetup.value3;
        FuelCapacityUpgrades[3] = dictionaryCapacitySetup.value4;

        FuelUpgradesCosts[0] = dictionaryCostSetup.value1;
        FuelUpgradesCosts[1] = dictionaryCostSetup.value2;
        FuelUpgradesCosts[2] = dictionaryCostSetup.value3;
        FuelUpgradesCosts[3] = dictionaryCostSetup.value4;
    }
}
