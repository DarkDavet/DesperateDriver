using UnityEngine;

[CreateAssetMenu(fileName = "GameSetups", menuName = "Data/Game Setups")]
public class GameSettings : ScriptableObject
{
    private int selectedlevel;
    public int SelectedLevel
    {
        get 
        {  
            return selectedlevel; 
        }
        set
        {
            selectedlevel = value;
        }
    }
}
