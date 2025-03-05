using UnityEngine;
using System.Collections.Generic;

public class UpgradeData: SingletonLocal<UpgradeData>
{
    [SerializeField] private List<UpgradeManager> upgradeManagerList;

    public Dictionary<string, int> UpgradeLevels { get; private set; } = new Dictionary<string, int>();

    public void InitDictionary(string key, int value)
    {
        UpgradeLevels.Add(key, value);
    }

    public void UpdateDictionary(string key, int value)
    {
        UpgradeLevels[key] = value;
    }

    public void SaveUpgradeData()
    {
        StorageManager.Instance.SaveUpgradeData(Instance);
    }

    public UpgradeDataData PackUpgradeData()
    {
        var data = new UpgradeDataData()
        {
            upgradeLevelsData = this.UpgradeLevels
        };
        return data;
    }

    public void UnpackUpgradeData(UpgradeDataData data)
    {
        this.UpgradeLevels = data.upgradeLevelsData;

        foreach (var manager in upgradeManagerList)
        {
            manager.GetUpgradelevelFromData(UpgradeLevels);
        }
    }
}

public class UpgradeDataData
{
    public Dictionary<string, int> upgradeLevelsData = new Dictionary<string, int>();
}
