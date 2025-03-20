using UnityEngine;

public class ZeroRepState: StatusState
{
    private PlayerStatusStateController _statusStateController;

    private RepSet _repSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private LevelInventory _levelInventory;
    private GameEvent m_MoneyLoseEvent;

    public ZeroRepState(PlayerStatusStateController statusStateController, RepSet repSet, UIStatusStateWidget uiStatusStateWidget, LevelInventory levelInventory, GameEvent moneyLoseEvent) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _repSet = repSet;
        _uiStatusStateWidget = uiStatusStateWidget;
        _levelInventory = levelInventory;
        m_MoneyLoseEvent = moneyLoseEvent;
    }

    public override void Enter()
    {
        //_statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        _uiStatusStateWidget.SetStatusStateSettings(_repSet.uiColor, _repSet.statusName);
        m_MoneyLoseEvent?.Raise();
        //_uiStatusStateWidget.SetupSliderValue(_statusSet.amountMaxLimit);
    }

    public override void Update()
    {
        /*if (_timer.Timer < _statusSet.amountMinLimit)
        {
            _statusStateController.SetState<SecondStatusState>();
        }
        _uiStatusStateWidget.UpdateWidget(_timer.Timer);*/
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
    }
}
