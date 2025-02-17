using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;

public class LevelManager : SingletonLocal<LevelManager>
{
    const string CompleteLevelCount_PrefsKey = "Complete Lvl Count";
    const string LastLevelIndex_PrefsKey = "Last Level Index";
    const string CurrentAttempt_PrefsKey = "Current Attempt";

    public static int CompleteLevelCount
    {
        get
        {
            return PlayerPrefs.GetInt(CompleteLevelCount_PrefsKey);
        }
        set
        {
            PlayerPrefs.SetInt(CompleteLevelCount_PrefsKey, value);
        }
    }
    public static int LastLevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt(LastLevelIndex_PrefsKey);
        }
        set
        {
            PlayerPrefs.SetInt(LastLevelIndex_PrefsKey, value);
        }
    }
    public static int CurrentAttempt
    {
        get
        {
            return PlayerPrefs.GetInt(CurrentAttempt_PrefsKey);
        }
        set
        {
            PlayerPrefs.SetInt(CurrentAttempt_PrefsKey, value);
        }
    }
    public int CurrentLevelIndex;

    [SerializeField] bool editorMode = false;
    [SerializeField] LevelsList levels;
    public List<Level> Levels => levels.lvls;

    public event Action OnLevelStarted;

    public void DefaultInit()
    {
#if !UNITY_EDITOR
            editorMode = false;
#endif
        if (!editorMode)
        {
            SelectLevel(LastLevelIndex, true);
        }
    }

    public void OnDestroy()
    {
        LastLevelIndex = CurrentLevelIndex;
        ClearDOTween();
    }

    private void OnApplicationQuit()
    {
        LastLevelIndex = CurrentLevelIndex;
        ClearDOTween();
    }

    public void StartLevel()
    {
        //Init();
        //OnLevelStarted?.Invoke();
    }

    public void RestartLevel()
    {
        SelectLevel(CurrentLevelIndex, false);
    }

    public void NextLevel()
    {
        SelectLevel(CurrentLevelIndex + 1);
    }

    public void SelectLevel(int levelIndex, bool indexCheck = true)
    {
        if (indexCheck)
            levelIndex = GetCorrectedIndex(levelIndex);

       // Debug.Log("Selected Level Index: " + levelIndex);

        if (Levels[levelIndex] == null)
        {
            Debug.Log("<color=red>There is no prefab attached!</color>");
            return;
        }

        var level = Levels[levelIndex];

        if (level)
        {
            ClearDOTween();
            SetLevelParams(level);
            CurrentLevelIndex = levelIndex;
            Debug.Log("CurrentLevelIndex updated: " + CurrentLevelIndex);
            Debug.Log("LastLevelIndex updated: " + LastLevelIndex);
        }
    }

    private void ClearDOTween()
    {
        DOTween.KillAll(); // Kill all tweens
        DOTween.Clear();   // Clear all tweens
    }


    public void PrevLevel() =>
            SelectLevel(CurrentLevelIndex - 1);

    private int GetCorrectedIndex(int levelIndex)
    {
        if (editorMode)
            return levelIndex > Levels.Count - 1 || levelIndex <= 0 ? 0 : levelIndex;
        else
        {
            int levelId = levelIndex;
            if (levelId > Levels.Count - 1)
            {
                if (levels.randomizedLvls)
                {
                    List<int> lvls = Enumerable.Range(0, levels.lvls.Count).ToList();
                    lvls.RemoveAt(CurrentLevelIndex);
                    return lvls[UnityEngine.Random.Range(0, lvls.Count)];
                }
                else return levelIndex % levels.lvls.Count;
            }
            return levelId;
        }
    }

    private void SetLevelParams(Level level)
    {
        if (level)
        {
            if (!Application.isPlaying)
            {
                ClearChildsEditorVer();
            }
            else
            {
                ClearChildsRunTimeVer();
            }
            //Debug.Log("Cleared previous level objects.");
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Instantiate(level, transform);
            }
            else
            {
                PrefabUtility.InstantiatePrefab(level, transform);
            }
#else
        Instantiate(level, transform);
#endif
            Debug.Log("Instantiated new level: " + level.name);
        }
    }


    private void ClearChildsEditorVer()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject destroyObject = transform.GetChild(i).gameObject;
            DestroyImmediate(destroyObject);
        }
    }

    private void ClearChildsRunTimeVer() 
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject child in children)
        {
            Destroy(child); 
        }
       // Debug.Log("Cleared all child objects.");
    }
}