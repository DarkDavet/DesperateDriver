using UnityEngine;

public class LevelEntrySetup : MonoBehaviour
{
    [SerializeField] private Material skyboxMat;
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private ItemsEventStation itemsEventStation;
    [SerializeField] private WinLoseEventStation winLoseEventStation;
    [SerializeField] private UILevelInventoryWidget uILevelInventoryWidget;

    private void Start()
    {
        itemsEventStation.Initialize();
        winLoseEventStation.Initialize();
        uILevelInventoryWidget.Init();
        levelInventory.OnLevelStart();
        SetSkybox(skyboxMat);
    }

    private void SetSkybox(Material skyboxMat)
    {
        RenderSettings.skybox = skyboxMat;
    }
}
