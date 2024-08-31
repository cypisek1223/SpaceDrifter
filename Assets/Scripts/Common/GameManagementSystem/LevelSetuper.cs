using System;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LevelSetuper : Singleton<LevelSetuper>
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FlyLateStart easyStart;
        [SerializeField] private Respawner respawner;   public Respawner Respawner => respawner;
        [SerializeField] private InGameMenu inGameMenu;

        protected override void Awake() 
        {
            base.Awake();

            LevelLoader.SceneFaded += OnLevelInitiated;
            LevelLoader.SceneLoaded += OnLevelLoaded;
            LevelLoader.SceneRevealed += OnLevelReady;

            GameManager.GamePaused += OnPaused;
            GameManager.GameResumed += OnResumed;
            GameManager.LevelFinished += OnLevelFinished;
            GameManager.PlayerKilled += OnPlayerKilled;
        }
        private void OnDestroy()
        {
            LevelLoader.SceneFaded -= OnLevelInitiated;
            LevelLoader.SceneLoaded -= OnLevelLoaded;
            LevelLoader.SceneRevealed -= OnLevelReady;

            GameManager.GamePaused -= OnPaused;
            GameManager.GameResumed -= OnResumed;
            GameManager.LevelFinished -= OnLevelFinished;
            GameManager.PlayerKilled -= OnPlayerKilled;
        }

        public void SetPlayerStartPos(Vector3 startPos, Quaternion startRot)
        {
            playerController.SetStartPosition(startPos, startRot);
        }

        private void OnPlayerKilled(LevelData level)
        {
            if(level.Type == LevelType.Generated)
            {

            }
            respawner.Respawn();
        }

        private void OnPaused()
        {
            playerController.Pause();
            ScoreKeeper.Instance.PauseTime();
        }
        private void OnResumed()
        {
            inGameMenu.Hide();

            Action callback = playerController.Unpause;
            callback += ScoreKeeper.Instance.ResumeTime;
            callback += inGameMenu.Show;
            easyStart.StartCountDown(callback);
        }

        //Methods for steering
        private void OnLevelInitiated(LevelData level)
        {
            Respawner.ResetEnctranceList();
            SplashManager.Instance.ResetSplashes();
            LevelManager.Instance.ResetLevel();

            switch (level.Type)
            {
                case LevelType.MainMenu:
                    easyStart.CancelCountDown();
                    ScoreKeeper.Instance.HideAll();
                    InputHandler.Instance.Deactivate();
                    playerController.gameObject.SetActive(false);
                    inGameMenu.Hide();
                    break;

                case LevelType.Arcade:
                case LevelType.Generated:
                    //Cyprian Coment it 
                    //playerController.SetInnerGlowColor(Color.red);
                    //playerController.SetInnerGlowColor(level.Planet.LightColor);
                    //CameraController.Instance.SetBackgroundMaterialAndColor(level.Planet.BackgroundMat, level.Planet.LightColor, level.Planet.DarkColor);
                    ScoreKeeper.Instance.InitLevel(level);
                    InputHandler.Instance.Activate();
                    CameraController.SetActive(true);
                    break;
            }
        }
        private void OnLevelLoaded(LevelData level)
        {
            switch (level.Type)
            {
                case LevelType.MainMenu:
                    CameraController.SetActive(false);
                    break;

                case LevelType.Arcade:
                case LevelType.Generated:
                    //Cyprian Coment it
                    //SplashManager.Instance.PaintAllSplashes(level.Planet.SplashMat, level.Planet.LightColor, level.Planet.DarkColor);
                    //LevelManager.Instance.PaintMaps(level.Planet.PrimaryColor);
                    //TUTAJ TRZEBA DODAC PRZELACZANIE KAMERY I PARTICLE
                    playerController.gameObject.SetActive(true);
                    playerController.Hide();
                    playerController.Pause();
                    CameraController.Instance.SetInitialPosition(respawner.Position);
                    inGameMenu.Show();
                    break;
            }
        }
        private void OnLevelReady(LevelData level)
        {
            switch (level.Type)
            {
                case LevelType.MainMenu:

                    break;

                case LevelType.Generated:
                case LevelType.Arcade:
                    //easyStart.StartCountDown(OnLevelStarted);
                    OnLevelStarted();
                    break;
            }
        }
        private void OnLevelStarted()// Only Arcade levels will "Start" and its when 321 counting ends
        {
            inGameMenu.Show();
            //playerController.Unpause();
            respawner.Respawn();
            ScoreKeeper.Instance.StartLevel();
            ArrowNavigation.Instance.ShowArrow();
        }

        private void OnLevelFinished()
        {
            inGameMenu.Hide();
            InputHandler.Instance.Deactivate();
            playerController.Hide();
            ScoreKeeper.Instance.FinishCurrentLevel();
            ArrowNavigation.Instance.HideArrow();
        }
    }
}