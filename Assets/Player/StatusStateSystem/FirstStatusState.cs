using UnityEngine;

public class FirstStatusState : StatusState
{
    private PlayerStatusStateController _statusStateController;

    private StatusSet _statusSet;
    private UIStatusStateWidget _uiStatusStateWidget;
    private LevelInventory _levelInventory;
    private MeshRenderer _carBody;
    public FirstStatusState(PlayerStatusStateController statusStateController, StatusSet statusSet, MeshRenderer carBody, UIStatusStateWidget uiStatusStateWidget, LevelInventory levelInventory) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _statusSet = statusSet;
        _uiStatusStateWidget = uiStatusStateWidget;
        _levelInventory = levelInventory;
        _carBody = carBody;
    }

    public override void Enter()
    {
        //_statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        
        _uiStatusStateWidget.SetStatusStateSettings(_statusSet.uiColor, _statusSet.statusName);
        _carBody.material = _statusSet.carMaterial;
    }

    public override void Update()
    {
        if (_levelInventory.CheckItemsAmount(ItemType.MONEY) > _statusSet.moneyMaxLimit)
        {
            _statusStateController.SetState<SecondStatusState>();
        }
    }

    public override void Exit()
    {
        //_statusSet.playerBody.SetActive(false);
    }
}
