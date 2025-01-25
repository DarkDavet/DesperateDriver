using UnityEngine;

public class FirstStatusState : StatusState
{
    private PlayerStatusStateController _statusStateController;

    private StatusSet _statusSet;
    private Animator _statusAnimator;
    private UIStatusStateWidget _uiStatusStateWidget;
    private LevelInventory _levelInventory;
    public FirstStatusState(PlayerStatusStateController statusStateController, StatusSet statusSet, Animator animator, UIStatusStateWidget uiStatusStateWidget, LevelInventory levelInventory) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _statusSet = statusSet;
        _statusAnimator = animator;
        _uiStatusStateWidget = uiStatusStateWidget;
        _levelInventory = levelInventory;
    }

    public override void Enter()
    {
        _statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        _uiStatusStateWidget.SetStatusStateSettings(_statusSet.uiColor, _statusSet.statusName);
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
        _statusSet.playerBody.SetActive(false);
    }
}
