using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusStateContext : MonoBehaviour
{
    [SerializeField] private List<StatusSet> _statusSets = new List<StatusSet>();
    [SerializeField] private Animator statusAnimator;
    [SerializeField] private UIPlayerManager uIPlayerManager;
    [SerializeField] private LevelInventory levelInventory;
    
    private PlayerStatusStateController statusStateController;

    private void Start()
    {
        statusStateController = new PlayerStatusStateController();

        statusStateController.AddState(new FirstStatusState(statusStateController, GetStatusSet("Poor"), statusAnimator, uIPlayerManager, levelInventory));
        statusStateController.AddState(new SecondStatusState(statusStateController, GetStatusSet("Middle"), statusAnimator, uIPlayerManager, levelInventory));
        statusStateController.AddState(new ThirdStatusState(statusStateController, GetStatusSet("Rich"), statusAnimator, uIPlayerManager, levelInventory));

        statusStateController.SetState<FirstStatusState>();
    }

    private void Update()
    {
        statusStateController?.Update();
    }

    private StatusSet GetStatusSet(string statusSetName)
    {
        foreach (var statusSet in _statusSets)
        {
            if(statusSet.statusName == statusSetName)
            {
                return statusSet;
            }
        }
        return null;
    }
}
