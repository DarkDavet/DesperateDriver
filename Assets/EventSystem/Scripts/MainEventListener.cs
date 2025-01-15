using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class MainEventListener : Singleton<MainEventListener>
{
    [SerializeField] private UIMoneyWidget uIMoneyWidget;
    [SerializeField] private UIPlayerManager uIPlayerManager;

    [SerializeField] private GameEventListener m_WinEventListener;
    [SerializeField] private GameEventListener m_LoseEventListener;
    [SerializeField] private GameEventListener m_PickMoneyEventListener;
    [SerializeField] private GameEventListener m_PickBottleEventListener;

    private int moneyAmount;
    void Start()
    {
        m_WinEventListener.EventHandler = OnWin;
        m_LoseEventListener.EventHandler = OnLose;
        m_PickMoneyEventListener.EventHandler = OnMoneyPick;
        m_PickBottleEventListener.EventHandler = OnBottlePick;
    }

    private void OnEnable()
    {
        m_WinEventListener.Subscribe();
        m_LoseEventListener.Subscribe();
        m_PickMoneyEventListener.Subscribe();
        m_PickBottleEventListener.Subscribe();
    }

    private void OnDisable()
    {
        m_WinEventListener.Unsubscribe();
        m_LoseEventListener.Unsubscribe();
        m_PickMoneyEventListener.Unsubscribe() ;
        m_PickBottleEventListener.Unsubscribe();
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {
        
    }

    public void OnMoneyPick()
    {
        moneyAmount = Random.Range(1, 20);
        uIMoneyWidget.UpdateWidget(moneyAmount);
        uIPlayerManager.OnMoneyAdded(moneyAmount);
        AudioManager.Instance.Play("AddMoney");
    }

    public void OnBottlePick()
    {
        moneyAmount = Random.Range(1, 20);
        uIMoneyWidget.UpdateWidget(-moneyAmount);
        uIPlayerManager.OnMoneyRemoved(moneyAmount);
        AudioManager.Instance.Play("RemoveMoney");
    }

}
