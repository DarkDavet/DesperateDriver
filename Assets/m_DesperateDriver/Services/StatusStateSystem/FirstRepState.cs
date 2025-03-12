using UnityEngine;

public class FirstRepState: StatusState
{
    private PlayerStatusStateController _statusStateController;

    private RepSet _repSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private LevelInventory _levelInventory;

    public FirstRepState(PlayerStatusStateController statusStateController, RepSet repSet, UIStatusStateWidget uiStatusStateWidget, LevelInventory levelInventory) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _repSet = repSet;
        _uiStatusStateWidget = uiStatusStateWidget;
        _levelInventory = levelInventory;
    }

    public override void Enter()
    {
        //_statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        _uiStatusStateWidget.SetStatusStateSettings(_repSet.uiColor, _repSet.statusName);
        _uiStatusStateWidget.SetupSliderValue(_levelInventory.lvlMoneyLimit);
    }

    public override void Update()
    {
        if (_levelInventory.Tmp_Money <= 0)
        {
            _statusStateController.SetState<ZeroRepState>();
        }
        if (_levelInventory.Tmp_Money >= _levelInventory.FirstLimit)
        {
            _levelInventory.CollectItems(1, ItemType.KEY);
            _statusStateController.SetState<SecondRepState>();
        }
        _uiStatusStateWidget.UpdateWidget(_levelInventory.Tmp_Money);
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
    }
}
