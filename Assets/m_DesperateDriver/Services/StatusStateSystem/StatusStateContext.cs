using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusStateContext : MonoBehaviour
{
    [SerializeField] private List<StatusSet> _statusSets = new List<StatusSet>();
    [SerializeField] private UIStatusStateWidget uiStatusStateWidget;
    [SerializeField] private ItemTimer _timer;
    [SerializeField] private ItemValue _value;
    
    private PlayerStatusStateController statusStateController;

    private void Start()
    {
        statusStateController = new PlayerStatusStateController();

        statusStateController.AddState(new FirstStatusState(statusStateController, GetStatusSet("Fresh"), uiStatusStateWidget, _timer, _value));
        statusStateController.AddState(new SecondStatusState(statusStateController, GetStatusSet("Well"), uiStatusStateWidget, _timer, _value));
        statusStateController.AddState(new ThirdStatusState(statusStateController, GetStatusSet("Bad"), uiStatusStateWidget, _timer, _value));

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
