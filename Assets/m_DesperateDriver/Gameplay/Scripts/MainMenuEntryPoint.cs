using UnityEngine;
using System.Collections.Generic;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private List<UpgradeManager> upgradeManagers;
    [SerializeField] private List<LevelInventory> levelInventories;
    [SerializeField] private List<UILevelWidget> levelWidgets;
    [SerializeField] private UpgradeData upgradeData;
    [SerializeField] private LevelInventoryData levelInventoryData;
    [SerializeField] private TransactionService transactionService;
    [SerializeField] private SettingsLoader settingsLoader;


    private void Start()
    {
        StorageManager.Instance.InitStorageManager();
        settingsLoader.Init();
        CarMatShop.Instance.InitShop();
        transactionService.InitService();

        // Initialize Upgrade Managers
        foreach (UpgradeManager upgradeManager in upgradeManagers)
        {
            upgradeManager.InitManager();
        }
        levelInventoryData.InitDictionary();

        StorageManager.Instance.LoadUpgradeData(upgradeData);
        StorageManager.Instance.LoadLevelItemsData(levelInventoryData);

        foreach (LevelInventory levelInventory in levelInventories)
        {
            levelInventory.Initialize();
        }

        /*foreach (UILevelWidget widget in levelWidgets)
        {
            widget.InitWidget();
        }*/

    }
}
