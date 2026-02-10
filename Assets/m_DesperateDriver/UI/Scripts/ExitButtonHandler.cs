using UnityEngine;
using UnityEngine.UI;

public class ExitButtonHandler : MonoBehaviour
{
    [SerializeField] private UIDialogWindow dialogWindow;
    [SerializeField] private WinLoseEventStation winLoseEventStation;
    public Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        winLoseEventStation.HideObjects();
        Pause();
        dialogWindow.SetupWindow(
           $"Are you sure you want to return to main menu?",
           () => LeaveGameplay(),
           () => Resume()
           );
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log("Leaving process cancelled.");
    }

    private void LeaveGameplay()
    {
        winLoseEventStation.OnLevelExit();
    }
}
