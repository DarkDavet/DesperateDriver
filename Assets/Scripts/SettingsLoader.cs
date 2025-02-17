using UnityEngine;
using System.Collections.Generic;

public class SettingsLoader : Singleton<SettingsLoader>
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
        if (level >= 0 && level <= lvlsList.Count)
        {
            gameSettings.SelectedLevel = level;
        }
        else
        {
            Debug.Log($"Incorrect level index: {gameSettings.SelectedLevel}");
        }
    }
}
