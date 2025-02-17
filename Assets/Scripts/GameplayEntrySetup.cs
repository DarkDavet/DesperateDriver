using UnityEngine;

public class GameplayEntrySetup : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    private void Start()
    {
        LevelInit();
    }

    private void LevelInit()
    {
        if (CheckLevelSelection())
        {
            LevelManager.Instance.SelectLevel(gameSettings.SelectedLevel, true);
            gameSettings.ResetSettings();
        }
        else
        {
            LevelManager.Instance.DefaultInit();
        }
    }

    private bool CheckLevelSelection()
    {
        return gameSettings.IsLevelSelected;
    }
}
