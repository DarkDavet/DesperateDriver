using UnityEngine;

public class LevelEntrySetup : MonoBehaviour
{
    [SerializeField] private Material skyboxMat;
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private ItemsEventStation itemsEventStation;
    [SerializeField] private WinLoseEventStation winLoseEventStation;
    [SerializeField] private UILevelInventoryWidget uILevelInventoryWidget;
    [SerializeField] private UIGameInventoryWidget uIGameInventoryWidget;
    [SerializeField] private UIPlayerManager uiPlayerManager;
    [SerializeField] private InventoryDisplay iceCreamInventoryDisplay;

    [SerializeField] private VehicleController vehicleController;

    private void Start()
    {
        Debug.Log($"LevelInventory Instance ID in LevelEntrySetup: {levelInventory.GetInstanceID()}");
        vehicleController.Init();
        iceCreamInventoryDisplay.Init();
        itemsEventStation.Initialize();
        winLoseEventStation.Initialize();
        uILevelInventoryWidget.Init();
        uIGameInventoryWidget.Init();
        uiPlayerManager.Init();
        levelInventory.OnLevelStart();

        SetSkybox(skyboxMat);
    }

    private void SetSkybox(Material skyboxMat)
    {
        RenderSettings.skybox = skyboxMat;
    }
}
