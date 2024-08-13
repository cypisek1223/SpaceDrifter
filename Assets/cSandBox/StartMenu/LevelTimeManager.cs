using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class LevelData
{
    public int levelId;
    public float lowTime;
    public float mediumTime;
    public int stars;
}

public class LevelTimeManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> levels;

    private void Awake()
    {
        LoadAllLevelData();
    }

    private void LoadAllLevelData()
    {
        foreach (LevelData level in levels)
        {
            level.stars = PlayerPrefs.GetInt($"Level_{level.levelId}__Stars", 0);
        }
    }

    public void SaveLevelCompletionTime(int levelId, float completionTime)
    {
        LevelData level = levels.Find(l => l.levelId == levelId);
        if(level != null)
        {
            level.stars = CalculateStars(completionTime, level);
            PlayerPrefs.SetInt($"Level_{levelId}_Stars", level.stars);
            PlayerPrefs.SetFloat($"Level_{levelId}_CompletionTime", completionTime);
            PlayerPrefs.Save();
        }
    }

    private int CalculateStars(float completionTime, LevelData level)
    {
       if(completionTime < level.lowTime)
       {
            return 1;
       } 
       else if(completionTime <= level.mediumTime)
       {
            return 2;
       }
       else 
       {
            return 3;
       }
    }

    public int GetStars(int levelId)
    {
        return PlayerPrefs.GetInt($"Level_{levelId}_Stars", 0);
    }

    
}