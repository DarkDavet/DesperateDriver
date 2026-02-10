using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class WinLoseEventStation : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hideObjects;

    [SerializeField] private UIWindowLvlResults resultWindow;
    [SerializeField] private UIMonologWindow monologWindow;

    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private InventoryObject iceCreamTrunk;
    [SerializeField] private GameEventListener m_WinEventListener;

    [SerializeField] private GameEventListener m_LoseFuelEventListener;
    [SerializeField] private GameEventListener m_LoseMoneyEventListener;
    [SerializeField] private GameEventListener m_LoseObstalceEventListener;

    private InventoryBroker inventoryBroker;
    private LevelManager levelManager;

    private MonoBehaviour[] sceneObjects;
    private List<IResetable> resetableCollection = new();

    public void Initialize()
    {
        m_WinEventListener.EventHandler = OnWin;

        m_LoseFuelEventListener.EventHandler = OnFuelLose;
        m_LoseMoneyEventListener.EventHandler = OnMoneyLose;
        m_LoseObstalceEventListener.EventHandler = OnObstacleLose;

        inventoryBroker = FindAnyObjectByType<InventoryBroker>();
        levelManager = FindAnyObjectByType<LevelManager>();

        sceneObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        resetableCollection.Add(levelInventory);
    }

    private void OnEnable()
    {
        m_WinEventListener.Subscribe();

        m_LoseFuelEventListener.Subscribe();
        m_LoseMoneyEventListener.Subscribe();
        m_LoseObstalceEventListener.Subscribe();
    }

    private void OnDisable()
    {
        m_WinEventListener.Unsubscribe();

        m_LoseFuelEventListener.Unsubscribe();
        m_LoseMoneyEventListener.Unsubscribe();
        m_LoseObstalceEventListener.Unsubscribe();
    }

    public void OnWin()
    {
        Time.timeScale = 0f;
        iceCreamTrunk.Clear();
        inventoryBroker.TransferInventoryData(levelInventory);
        StorageManager.Instance.SaveGameInventoryData();
        levelInventory.SetGlobalData();
        HideObjects();

        resultWindow.SetupWindow(
            levelInventory,
            inventoryBroker,
            () => OnNextlevel(),
            () => OnLevelExit(),
            () => OnRestartlevel()
        );

    }

    private void OnRestartlevel()
    {
        UnityEngine.Debug.Log($"Time.timeScale before restarting: {Time.timeScale}");
        Time.timeScale = 1f;
        iceCreamTrunk.Clear();
        levelInventory.ResetObject();
        Debug.Log($"Time.timeScale after restarting: {Time.timeScale}");
        levelManager.RestartLevel();
    }

    private void OnNextlevel()
    {
        levelInventory.ResetObject();
        Time.timeScale = 1f;
        levelManager.NextLevel();
    }

    public void OnLevelExit()
    {
        iceCreamTrunk.Clear();
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadMainMenuScene();
    }

    public void HideObjects()
    {
        foreach (var obj in _hideObjects)
        {
            obj.SetActive(false);
        }
    }

    public void OnLose()
    {
        OnRestartlevel();
    }

    public void OnFuelLose()
    {
        Debug.Log("Fuel Lose");
        HideObjects();
        Time.timeScale = 0f;
        monologWindow.SetupWindow(
            "Your fuel level is empty :(",
            "Restart",
            () => OnLose()
            );
    }

    public void OnMoneyLose()
    {
        Debug.Log("Money Lose");
        HideObjects();
        Time.timeScale = 0f;
        monologWindow.SetupWindow(
            "You have no money :(",
            "Restart",
            () => OnLose()
            );
    }

    public void OnObstacleLose()
    {
        Debug.Log("Obstacle Lose");
        HideObjects();
        Time.timeScale = 0f;
        monologWindow.SetupWindow(
            "You got into car accident :(",
            "Restart",
            () => OnLose()
            );
    }



    public void ResetLevel()
    {

        foreach (MonoBehaviour obj in sceneObjects)
        {
            if (obj is IResetable)
            {
                resetableCollection.Add(obj as IResetable);
            }
        }

        foreach (IResetable obj in resetableCollection)
        {
            obj.ResetObject();
        }

        //resetableCollection.Clear();
    }
}
