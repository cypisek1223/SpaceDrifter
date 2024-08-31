using System;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private DataManager dataManager;

        public static event Action GamePaused;
        public static event Action GameResumed;
        public static event Action LevelFinished;
        public static event Action<LevelData> PlayerKilled;

        //To add Game Finished and Started events, etc...

    

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
            LevelLoader.LoadLevel( dataManager.CurrentLevel ); // Need to add condition checking if game first starts -> then just load main menu
            //(there;s such a problem bcuz 'InMenu' option could be unticked during build
        }

        public static void FinishLevel()
        {
            PauseGame();
            LevelFinished?.Invoke();

            //Level unlocking
            var dm = Instance.dataManager;
            if (dm.CurrentPlanet.IsLastLevel())
            {
                if (dm.IsLastPlanet())
                {
                    //Finish of The Game
                    return;
                }
                //Unlock next planet
                dm.NextPlanet.Unlock();
            }
            else
            {
                //Unlock next level
                dm.CurrentPlanet.NextLevel.Unlock();
            }
        }

        public static void PauseGame()
        {
            //Time.timeScale = 0;
            GamePaused?.Invoke();
        }
        public static void ResumeGame()
        {
            //Time.timeScale = 1;
            GameResumed?.Invoke();
        }
        public static void KillPlayer()
        {
            if (Instance.dataManager.CurrentLevel.Type == LevelType.Generated)
                LevelLoader.LoadLevel(Instance.dataManager.CurrentLevel);
            else
                PlayerKilled?.Invoke(Instance.dataManager.CurrentLevel);
        }

        public static void GoToMainMenu()
        {
            Instance.dataManager.InMenu = true;
            LevelLoader.LoadLevel( Instance.dataManager.MainMenu );
        }

        public static void RestartLevel()
        {
            LoadLevel(Instance.dataManager.CurrentLevel);
        }

        public static void SetCurrentPlanet(PlanetData planet)
        {
            Instance.dataManager.SetCurrentPlanet(planet);
        }
        public static void LoadLevel(LevelData level)
        {
            Instance.dataManager.InMenu = false;
            SetCurrentPlanet(level.Planet);
            Instance.dataManager.CurrentPlanet.SetCurrentLevel(level);       
            LevelLoader.LoadLevel(level);
        }
        //CYPRIAN ADDED THIS
        public static void LevelLoad(LevelData level, PlanetData planet)
        {
            //Instance.dataManager.InMenu = false;            
            SetCurrentPlanet(planet);
            Instance.dataManager.CurrentPlanet.SetCurrentLevel(level);
            LevelLoader.LoadLevel(level);
        }
        //END
        public static void NextLevel()
        {
            var dm = Instance.dataManager;
            LevelData nextLevel;
            if( dm.CurrentPlanet.IsLastLevel() )
            {
                if( dm.IsLastPlanet() )
                {
                    //Finish The Game
                    return;
                }

                //Finish the planet
                dm.SetCurrentPlanet( dm.NextPlanet );
                nextLevel = dm.MainMenu;
                dm.InMenu = true;
            }
            else
            {
                //Just go to next
                nextLevel = dm.CurrentPlanet.NextLevel;
                dm.CurrentPlanet.SetCurrentLevel(nextLevel);
            }

            LevelLoader.LoadLevel( nextLevel );
        }
    } 
}
