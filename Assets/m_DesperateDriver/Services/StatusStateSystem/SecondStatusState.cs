using UnityEngine;

public class SecondStatusState : StatusState
{
    private PlayerStatusStateController _statusStateController;

    private StatusSet _statusSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private ItemTimer _timer;
    private ItemValue _value;


    public SecondStatusState(PlayerStatusStateController statusStateController, StatusSet statusSet, UIStatusStateWidget uiStatusStateWidget, ItemTimer timer, ItemValue value) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _statusSet = statusSet;
        _uiStatusStateWidget = uiStatusStateWidget;
        _timer = timer;
        _value = value;
    }

    public override void Enter()
    {
        //_statusSet.carMaterial.SetActive(true);
        _uiStatusStateWidget.SetStatusStateSettings(_statusSet.uiColor, _statusSet.statusName);
        _value.Value = _statusSet.value;
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        // change color hud method to UIPlayerManager
    }

    public override void Update()
    {
        if (_timer.Timer < _statusSet.amountMinLimit)
        {
            _statusStateController.SetState<ThirdStatusState>();
        }
        if (_timer.Timer > _statusSet.amountMaxLimit)
        {
            _statusStateController.SetState<FirstStatusState>();
        }
        _uiStatusStateWidget.UpdateWidget(_timer.Timer);
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
    }
}
