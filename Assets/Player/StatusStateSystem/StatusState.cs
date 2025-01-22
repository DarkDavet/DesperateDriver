using UnityEngine;

public abstract class StatusState
{
    protected readonly PlayerStatusStateController StatusStateController;

    public StatusState(PlayerStatusStateController statusStateController)
    {
        StatusStateController = statusStateController;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
