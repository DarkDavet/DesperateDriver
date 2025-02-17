using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ItemsEventStation: MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private UIPlayerManager uIPlayerManager;

    [SerializeField] private GameEventListener m_PickMoneyEventListener;
    [SerializeField] private GameEventListener m_PickBottleEventListener;

    private int genereatedAmount;
    void Start()
    {
        m_PickMoneyEventListener.EventHandler = OnMoneyPick;
        m_PickBottleEventListener.EventHandler = OnBottlePick;

        levelInventory.Initialize();
    }

    private void OnEnable()
    {
        m_PickMoneyEventListener.Subscribe();
        m_PickBottleEventListener.Subscribe();
    }

    private void OnDisable()
    {
        m_PickMoneyEventListener.Unsubscribe() ;
        m_PickBottleEventListener.Unsubscribe();
    }

    public void OnMoneyPick()
    {
        genereatedAmount = Random.Range(1, 20);
        levelInventory.CollectItems(genereatedAmount, ItemType.MONEY);
        uIPlayerManager.OnMoneyAdded(genereatedAmount);
        AudioManager.Instance.Play("AddMoney");
    }

    public void OnBottlePick()
    {
        genereatedAmount = Random.Range(-20, -1);
        levelInventory.CollectItems(genereatedAmount, ItemType.MONEY);
        uIPlayerManager.OnMoneyRemoved(genereatedAmount);
        AudioManager.Instance.Play("RemoveMoney");
    }

}
