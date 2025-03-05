using UnityEngine;
using System.Collections.Generic;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private List<UpgradeManager> upgradeManagers;
    [SerializeField] private UpgradeData upgradeData;


    private void Start()
    {
        StorageManager.Instance.InitStorageManager();
        // Initialize Upgrade Managers
        foreach (UpgradeManager upgradeManager in upgradeManagers)
        {
            upgradeManager.InitManager();
        }

        StorageManager.Instance.LoadUpgradeData(upgradeData);
        CarMatShop.Instance.InitShop();
    }
}
