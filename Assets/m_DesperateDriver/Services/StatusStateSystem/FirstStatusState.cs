using UnityEngine;

public class FirstStatusState : StatusState
{
    private PlayerStatusStateController _statusStateController;

    private StatusSet _statusSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private ItemTimer _timer;
    private ItemValue _value;
    public FirstStatusState(PlayerStatusStateController statusStateController, StatusSet statusSet, UIStatusStateWidget uiStatusStateWidget, ItemTimer timer, ItemValue value) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _statusSet = statusSet;
        _uiStatusStateWidget = uiStatusStateWidget;
        _timer = timer;
        _value = value;
    }

    public override void Enter()
    {
        //_statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        if (!_timer.IsTimerLaunched)
        {
            _timer.SetupTimer(_statusSet.amountMaxLimit);
        }

        _uiStatusStateWidget.SetStatusStateSettings(_statusSet.uiColor, _statusSet.statusName);
        _uiStatusStateWidget.SetupSliderValue(_statusSet.amountMaxLimit);
        _value.Value = _statusSet.value;
    }

    public override void Update()
    {
        if (_timer.Timer < _statusSet.amountMinLimit)
        {
            _statusStateController.SetState<SecondStatusState>();
        }
        _uiStatusStateWidget.UpdateWidget(_timer.Timer);
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
    }
}
