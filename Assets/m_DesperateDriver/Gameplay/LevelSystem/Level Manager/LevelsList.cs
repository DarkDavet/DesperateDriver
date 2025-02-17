using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Data/Lvls List")]
    public class LevelsList : ScriptableObject
    {
        public bool randomizedLvls;
        public List<Level> lvls;
    }