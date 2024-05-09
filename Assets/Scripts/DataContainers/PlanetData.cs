using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

namespace SpaceDrifter2D
{
    [CreateAssetMenu(fileName = "Planet", menuName = "GameManagement/Planet")]
    public class PlanetData : ScriptableObject
    {
        #region Access Properties
        public string Name => name;
        public LevelData CurrentLevel { get { return levelList[currentLevel]; } }
        public List<LevelData> AllLevels => levelList.ToList();
        public LevelData LastLevel => levelList.Last();
        public LevelData NextLevel => IsLastLevel() ? null : levelList[currentLevel + 1];
        public bool Locked => locked;

        public Material BackgroundMat => backgroundMat;
        public Material SplashMat => splashMat;
        public Color PrimaryColor => themePrimaryColor;
        public Color LightColor => themeLightColor;
        public Color DarkColor => themeDarkColor; 
        #endregion

        [Header("Basic")]
        [SerializeField] new private string name;
        [SerializeField][Multiline] private string description;

        [Header("Visual Style")]
        [SerializeField] private Material backgroundMat;
        [SerializeField] private Material splashMat;
        /*[ColorUsage(true, true)]*/[SerializeField] private Color themePrimaryColor;
        /*[ColorUsage(true, true)]*/[SerializeField] private Color themeLightColor;
        /*[ColorUsage(true, true)]*/[SerializeField] private Color themeDarkColor;

        [Header("Levels and Availability")]
        [NaughtyAttributes.ReorderableList][SerializeField] private LevelData[] levelList;
        [SerializeField] private bool locked = true;

        int currentLevel;

        public void AssignPlanetLevels()
        {
            levelList[0].Unlock();

            foreach (var l in levelList)
            {
                l.SetPlanet(this);
            }
        }

        public bool IsLastLevel()
        {
            return CurrentLevel == LastLevel;
        }

        public void SetCurrentLevel(LevelData level)
        {
            //currentLevel = Array.IndexOf( levelList, Array.Find(levelList, l => l == level) );
            currentLevel = Array.IndexOf( levelList, level );
        }

        public void Unlock()
        {
            locked = false;
        }
    }
}