using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject mainWindow;

        public void Show()
        {
            pauseButton.SetActive(true);
            mainWindow.SetActive(false);
        }

        public void Hide()
        {
            pauseButton.SetActive(false);
            mainWindow.SetActive(false);
        }

        public void Pause()
        {
            pauseButton.SetActive(false);
            mainWindow.SetActive(true);
            GameManager.PauseGame();
        }

        public void Resume()
        {
            pauseButton.SetActive(true);
            mainWindow.SetActive(false);
            GameManager.ResumeGame();
        }

        public void MainMenu()
        {
            //Save data or smth first
            //Cleanup maybe?
            GameManager.GoToMainMenu();
        }

        public void NextLevel()
        {
            GameManager.NextLevel();
        }
    }
}