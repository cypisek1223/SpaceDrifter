using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace SpaceDrifter2D
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        public LevelData CurrentLevelData => targetLevel ?? currentLevel;

        public static event Action<LevelData> SceneLoadingInitiated;
        public static event Action<LevelData> SceneFaded;
        public static event Action<LevelData> SceneLoaded;
        public static event Action<LevelData> SceneRevealed;

        public LoadingScreen loadingScreen;
        public ScreenFader screenFader;

        public LevelData targetLevel;
        LevelData currentLevel;

        AsyncOperation asyncLoading;
        AsyncOperation asyncUnloading;

        [SerializeField] private GameObject gamePlayCamera;
        public static void LoadLevel(LevelData level)
        {
            Debug.Log("LOAD LEVEL");
            SceneLoadingInitiated?.Invoke( Instance.targetLevel );

            Instance.targetLevel = level;
            //Cyprian Add this
            //if(Instance.gamePlayCamera != null)
            //{
            //    Instance.gamePlayCamera.SetActive(true);
            //}
            //END
            if (Instance.currentLevel != null && Instance.currentLevel.Fade ) // no currentLevel means we just started the Game
            {
                Debug.Log("IF");
                //Begin fading and wait for it. Begin actual loading when faded

                //SOMETHING NEEDS TO CHANGE TO WORK THE SAME AS SEBASTIAN'S
                Instance.screenFader.gameObject.SetActive(true);
                Instance.screenFader.Fade( Instance.BeginLoading );
            }
            else
            {
                Debug.Log("ELSE");
                Instance.BeginLoading();
            }

            //TODO: disable all interactivity
        }

        private void BeginLoading()
        {
            SceneFaded?.Invoke(targetLevel);
            
            //Unload previous level
            if ( currentLevel != null )
            {
                asyncUnloading = SceneManager.UnloadSceneAsync(currentLevel.Scene); // Consider using different mode
            }

            //Load next level
            asyncLoading = SceneManager.LoadSceneAsync(targetLevel.Scene, LoadSceneMode.Additive);

            loadingScreen.Process( new AsyncOperation[]{ asyncUnloading, asyncLoading }, OnLevelLoaded );
            
            screenFader.gameObject.SetActive(false);
        }

        private void OnLevelLoaded()
        {
            SceneLoaded?.Invoke(targetLevel);
            
            if ( targetLevel.Fade )
            {
                screenFader.gameObject.SetActive(true);
                screenFader.Reveal(OnLevelRevealed);
            }
            else
            {
                OnLevelRevealed();
            }
        }

        private void OnLevelRevealed() //Using this because the level might be loaded first but is still hidden by ScreenFader which reveals it
        {
            screenFader.gameObject.SetActive(false);
            SceneRevealed?.Invoke(targetLevel);
            currentLevel = targetLevel;
        }
    }
}
