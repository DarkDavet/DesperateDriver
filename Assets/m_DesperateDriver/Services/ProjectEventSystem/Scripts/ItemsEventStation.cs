
using UnityEngine;

public class ItemsEventStation : MonoBehaviour
{
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private UIPlayerManager uIPlayerManager;
    [SerializeField] private FuelTank fuelTank;

    [SerializeField] private GameEventListener m_PickMoneyEventListener;
    [SerializeField] private GameEventListener m_PickBottleEventListener;
    [SerializeField] private GameEventListener m_PickFuelEventListener;
    [SerializeField] private GameEventListener m_UseGasStationEventListener;


    private int genereatedAmount;
    public void Initialize()
    {
        m_PickMoneyEventListener.EventHandler = OnMoneyPick;
        m_PickBottleEventListener.EventHandler = OnBottlePick;
        m_PickFuelEventListener.EventHandler = OnFuelPick;
        m_UseGasStationEventListener.EventHandler = OnGasStationUse;

        levelInventory.OnItemsAdded += OnMoneyEarned;
        levelInventory.OnItemsRemoved += OnMoneyRemoved;
    }

    private void OnEnable()
    {
        m_PickMoneyEventListener.Subscribe();
        m_PickBottleEventListener.Subscribe();
        m_PickFuelEventListener.Subscribe();
        m_UseGasStationEventListener.Subscribe();
    }

    private void OnDisable()
    {
        m_PickMoneyEventListener.Unsubscribe();
        m_PickBottleEventListener.Unsubscribe();
        m_PickFuelEventListener.Unsubscribe();
        m_UseGasStationEventListener.Unsubscribe();
    }

    public void OnMoneyEarned(int amount)
    {
        uIPlayerManager.OnMoneyAdded(amount);
    }

    public void OnMoneyRemoved(int amount)
    {
        uIPlayerManager.OnMoneyRemoved(amount);
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

    public void OnFuelPick()
    {
        fuelTank.IncreaseFuelLevel(5);
    }

    public void OnGasStationUse()
    {
        if ((fuelTank.CurrentFuel < fuelTank.MaxFuel) && levelInventory.RequestPayment(1))
        {
            fuelTank.IncreaseFuelLevel(1f);
            Debug.Log($"Current fuel level: {fuelTank.CurrentFuel}");
            Debug.Log($"Max fuel level: {fuelTank.MaxFuel}");
        }
    }
}
