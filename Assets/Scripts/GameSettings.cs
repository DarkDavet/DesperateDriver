using UnityEngine;

[CreateAssetMenu(fileName = "GameSetups", menuName = "Data/Game Setups")]
public class GameSettings : ScriptableObject
{
    [SerializeField, ReadOnly] private int selectedLevel;
    [SerializeField, ReadOnly] private bool isLevelSelected;

    public int SelectedLevel
    {
        get => selectedLevel;
        set => selectedLevel = value;
    }

    public bool IsLevelSelected
    {
        get => isLevelSelected;
        set => isLevelSelected = value;
    }

    public void ResetSettings()
    {
        isLevelSelected = false;
    }
}
