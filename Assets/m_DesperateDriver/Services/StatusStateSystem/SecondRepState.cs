using UnityEngine;

public class SecondRepState: StatusState
{
    private PlayerStatusStateController _statusStateController;

    private RepSet _repSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private LevelInventory _levelInventory;

    public SecondRepState(PlayerStatusStateController statusStateController, RepSet repSet, UIStatusStateWidget uiStatusStateWidget, LevelInventory levelInventory) : base(statusStateController)
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
        //_uiStatusStateWidget.SetupSliderValue(_statusSet.amountMaxLimit);
    }

    public override void Update()
    {
        if (_levelInventory.Tmp_Money < _levelInventory.FirstLimit)
        {
            _levelInventory.CollectItems(-1, ItemType.KEY);
            _statusStateController.SetState<FirstRepState>();
        }
        if (_levelInventory.Tmp_Money >= _levelInventory.SecondLimit)
        {
            _levelInventory.CollectItems(1, ItemType.KEY);
            _statusStateController.SetState<ThirdRepState>();
        }
        _uiStatusStateWidget.UpdateWidget(_levelInventory.Tmp_Money);
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
        
    }
}
