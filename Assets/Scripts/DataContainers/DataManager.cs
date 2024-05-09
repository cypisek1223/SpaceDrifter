using System;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    [CreateAssetMenu(fileName = "DataManager", menuName = "GameManagement/DataManager")]
    public class DataManager : ScriptableObject
    {
        public PlanetData CurrentPlanet => planetsList[currentPlanet];
        public LevelData CurrentLevel => inMenu ? mainMenu : planetsList[currentPlanet].CurrentLevel;
        public LevelData MainMenu => mainMenu;
        public PlanetData NextPlanet => IsLastPlanet() ? null : planetsList[currentPlanet + 1];
        public bool InMenu { get { return inMenu; } set { inMenu = value; } }

        public bool GameFirstStart { get; set; } = true; // Needed to notify menu not to open Main Panel on PlanetSwitch

        [SerializeField] private PlanetData[] planetsList;
        [SerializeField] private LevelData mainMenu;
        [SerializeField] private bool inMenu = true;
        private int currentPlanet;

        private void OnValidate()
        {
            //Consider removing it in build. Probably needed only for testing
            GameFirstStart = true;
        }

        public bool IsLastPlanet()
        {
            return currentPlanet == planetsList.Length - 1;
        }

        public void SetCurrentPlanet(PlanetData planet)
        {
            int index = Array.IndexOf(planetsList, Array.Find(planetsList, p => p == planet) );

            if(index < 0)
            {
                throw new SystemException($"GlobalGameState couldn't find {planet.Name} planet on the list");
            }

            currentPlanet = index;
        }
    }
}