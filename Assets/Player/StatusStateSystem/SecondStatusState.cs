using UnityEngine;

public class SecondStatusState : StatusState
{
    private PlayerStatusStateController _statusStateController;

    private StatusSet _statusSet;
    private Animator _statusAnimator;
    private UIPlayerManager _uiPlayerManager;
    private LevelInventory _levelInventory;

    public SecondStatusState(PlayerStatusStateController statusStateController, StatusSet statusSet, Animator animator, UIPlayerManager uiPlayer, LevelInventory levelInventory) : base(statusStateController)
    {
        _statusStateController = statusStateController;

        _statusSet = statusSet;
        _statusAnimator = animator;
        _uiPlayerManager = uiPlayer;
        _levelInventory = levelInventory;
    }

    public override void Enter()
    {
        _statusSet.playerBody.SetActive(true);
        //_statusAnimator.SetTrigger(_statusSet.tagAnimTrigger);
        // change color hud method to UIPlayerManager
    }

    public override void Update()
    {
        if (_levelInventory.CheckItemsAmount(ItemType.MONEY) > _statusSet.moneyMaxLimit)
        {
            _statusStateController.SetState<ThirdStatusState>();
        }
        if (_levelInventory.CheckItemsAmount(ItemType.MONEY) < _statusSet.moneyMinLimit)
        {
            _statusStateController.SetState<FirstStatusState>();
        }
    }

    public override void Exit()
    {
        _statusSet.playerBody.SetActive(false);
    }
}
