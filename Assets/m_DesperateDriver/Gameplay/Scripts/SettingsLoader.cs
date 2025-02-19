using UnityEngine;
using System.Collections.Generic;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private LevelsList levelsList;

    private List<Level> lvlsList;

    private void Start()
    {
        lvlsList = levelsList.lvls;
    }

    public void SetLoadedLevel(int level)
    {
        if (level >= 1 && level <= lvlsList.Count)
        {
            gameSettings.SelectedLevel = level;
            gameSettings.IsLevelSelected = true;
        }
        else
        {
            Debug.Log($"Incorrect level index: {gameSettings.SelectedLevel}");
        }
        SceneLoader.Instance.LoadGameplayScene();
    }
}
