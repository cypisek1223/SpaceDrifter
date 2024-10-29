using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace SpaceDrifter2D
{
    [System.Serializable]
    public class LevelStats
    {
        public string levelName;
        public int stars;
        public float bestTime;
    }

    public class PlayerPrefsViewer : MonoBehaviour
    {
        //[SerializeField]
        //private List<LevelStats> levelStatsList;
        //
        //private void Start()
        //{
        //    LoadLevelStats();
        //}
        //
        //
        /////PRZEJRZYJ TEN SKRYPT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //private void LoadLevelStats()
        //{
        //    levelStatsList = new List<LevelStats>();
        //
        //    for (int i = 0; i < ScoreKeeper.Instance.levels.Count; i++)
        //    {
        //        var level = ScoreKeeper.Instance.levels[i];
        //        int stars = PlayerPrefs.GetInt($"Level_{level.levelId}_Stars", 0);
        //        float bestTime = PlayerPrefs.GetFloat($"Level_{level.levelId}_TheBestTime", 0f);
        //
        //        LevelStats stats = new LevelStats
        //        {
        //            levelName = $"Level_{level.levelId}",
        //            stars = stars,
        //            bestTime = bestTime
        //        };
        //
        //        levelStatsList.Add(stats);
        //    }
        //}
    }
}
