using System.Collections.Generic;
using UnityEngine;

public class RepStatusStateContext : MonoBehaviour
{
    [SerializeField] private List<RepSet> _repSets = new List<RepSet>();
    [SerializeField] private UIStatusStateWidget uiStatusStateWidget;
    [SerializeField] private LevelInventory levelInventory;
    [SerializeField] private GameEvent m_MoneyLoseEvent;

    private PlayerStatusStateController statusStateController;

    private void Start()
    {
        statusStateController = new PlayerStatusStateController();

        statusStateController.AddState(new ZeroRepState(statusStateController, GetRepSet("Looser"), uiStatusStateWidget, levelInventory, m_MoneyLoseEvent));
        statusStateController.AddState(new FirstRepState(statusStateController, GetRepSet("Nobody"), uiStatusStateWidget, levelInventory));
        statusStateController.AddState(new SecondRepState(statusStateController, GetRepSet("Beginner"), uiStatusStateWidget, levelInventory));
        statusStateController.AddState(new ThirdRepState(statusStateController, GetRepSet("Expierenced"), uiStatusStateWidget, levelInventory));
        statusStateController.AddState(new FourthRepState(statusStateController, GetRepSet("Pro"), uiStatusStateWidget, levelInventory));
        statusStateController.AddState(new PlatinumRepState(statusStateController, GetRepSet("Ace"), uiStatusStateWidget, levelInventory));

        statusStateController.SetState<FirstRepState>();
    }

    private void Update()
    {
        statusStateController?.Update();
    }

    private RepSet GetRepSet(string repSetName)
    {
        foreach (var repSet in _repSets)
        {
            if (repSet.statusName == repSetName)
            {
                return repSet;
            }
        }
        return null;
    }
}
