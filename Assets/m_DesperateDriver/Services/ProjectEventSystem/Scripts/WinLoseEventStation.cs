using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class WinLoseEventStation: MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private UIMoneyWidget uIMoneyWidget;
    [SerializeField] private UIPlayerManager uIPlayerManager;
    [SerializeField] private GameEventListener m_WinEventListener;
    [SerializeField] private GameEventListener m_LoseEventListener;

    private StorageManager storageManager;
    private InventoryBroker inventoryBroker;
    private LevelManager levelManager;

    private MonoBehaviour[] sceneObjects;
    private List<IResetable> resetableCollection = new();

    void Start()
    {
        m_WinEventListener.EventHandler = OnWin;
        m_LoseEventListener.EventHandler = OnLose;

        storageManager = FindAnyObjectByType<StorageManager>();
        inventoryBroker = FindAnyObjectByType<InventoryBroker>();
        levelManager = FindAnyObjectByType<LevelManager>();

        sceneObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        resetableCollection.Add(levelInventory);
    }

    private void OnEnable()
    {
        m_WinEventListener.Subscribe();
        m_LoseEventListener.Subscribe();
    }

    private void OnDisable()
    {
        m_WinEventListener.Unsubscribe();
        m_LoseEventListener.Unsubscribe();
    }

    public void OnWin()
    {
        levelManager.NextLevel();
        Debug.Log("NextLevel called from WinLoseEventStation.");
        inventoryBroker.TransferInventoryData();
        storageManager.SaveGameInventoryData();
    }

    public void OnLose()
    {
        levelManager.RestartLevel();
        //ResetLevel();
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
