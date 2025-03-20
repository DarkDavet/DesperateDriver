using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowLvlResults : MonoBehaviour
{
    [SerializeField] private List<GameObject> visualElements;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI profitScore;

    [SerializeField] private TextMeshProUGUI limit1Score;
    [SerializeField] private TextMeshProUGUI limit2Score;
    [SerializeField] private TextMeshProUGUI limit3Score;
    [SerializeField] private TextMeshProUGUI limitLastScore;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;

    private Action onContinueAction;
    private Action onExitAction;
    private Action onRestartAction;

    private LevelInventory levelInventory;

    public void SetupWindow(LevelInventory levelInventory, InventoryBroker inventoryBroker, Action onContinue, Action onExit, Action onRestart)
    {
        gameObject.SetActive(true);
        this.levelInventory = levelInventory;
        title.text = $"Results of Level {levelInventory.Id}";
        currentScore.text = $"{levelInventory.Tmp_Money} $";
        bestScore.text = $"{levelInventory.Glb_Money} $";
        profitScore.text = $"{inventoryBroker.TransferedMoney} $";
        SetVisulaObjects();

        limit1Score.text = $"{levelInventory.FirstLimit} $";
        limit2Score.text = $"{levelInventory.SecondLimit} $";
        limit3Score.text = $"{levelInventory.ThirdLimit} $";
        limitLastScore.text = $"{levelInventory.lvlMoneyLimit} $";

        SetReachedLimit();

        onContinueAction = onContinue;
        onExitAction = onExit;
        onRestartAction = onRestart;

        continueButton.onClick.AddListener(Continue);
        exitButton.onClick.AddListener(Exit);
        restartButton.onClick.AddListener(Restart);
    }

    private void SetReachedLimit()
    {
        if (levelInventory.Glb_Money >= levelInventory.FirstLimit)
        {
            limit1Score.text = $"reached";
        }
        if (levelInventory.Glb_Money >= levelInventory.SecondLimit)
        {
            limit2Score.text = $"reached";
        }
        if (levelInventory.Glb_Money >= levelInventory.ThirdLimit)
        {
            limit3Score.text = $"reached";
        }
        if (levelInventory.Glb_Money == levelInventory.lvlMoneyLimit)
        {
            limitLastScore.text = $"reached";
        }
    }

    private void Continue()
    {
        onContinueAction?.Invoke(); // Invoke the confirm action
        gameObject.SetActive(false);
    }

    private void Exit()
    {
        onExitAction?.Invoke(); // Invoke the cancel action
        gameObject.SetActive(false); // Hide the window
    }

    private void Restart()
    {
        onRestartAction?.Invoke();
        gameObject.SetActive(false);
    }

    private void SetVisulaObjects()
    {
        switch (levelInventory.Glb_Stars)
        {
            case 0:
                visualElements[0].SetActive(false);
                visualElements[1].SetActive(false);
                visualElements[2].SetActive(false);
                break;
            case 1:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(false);
                visualElements[2].SetActive(false);
                break;
            case 2:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(true);
                visualElements[2].SetActive(false);
                break;
            case 3:
                visualElements[0].SetActive(true);
                visualElements[1].SetActive(true);
                visualElements[2].SetActive(true);
                break;
        }
    }
}
