using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private DataManager dataManager;

        [Header("Referenced Systems")]
        [SerializeField] private MenuCameraController menuCameraController;
        [SerializeField] private PlanetListController planetsList;

        [Header("Menu Panels and Windows")]
        [SerializeField] private GameObject mainWindow;
        [SerializeField] private GameObject settingsWindow;

        [Header("Camera Targets")]
        [SerializeField] private Transform mainWindowTarget;

        private Animator animator;

        private void Awake()
        {
            DisableAllWindows();

            if(dataManager.GameFirstStart)
            {
                mainWindow.SetActive(true);
                menuCameraController.SetTarget(mainWindowTarget);
                dataManager.GameFirstStart = false;
            }
            else
            {
                Planets();
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void GoToPlanets()
        {
            StartCoroutine(Hide(Planets));
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.Exit(0);
#else
Application.Quit();
#endif
        }
        private void Planets()
        {
            DisableAllWindows();
            planetsList.gameObject.SetActive(true);
        }
        private void Settings()
        {
            DisableAllWindows();
            settingsWindow.SetActive(true);
        }
        private IEnumerator Hide(Action callback)
        {
            animator.SetTrigger("out");
            yield return new WaitForSeconds(0.25f);
            callback?.Invoke();
        }

        private void DisableAllWindows()
        {
            planetsList.gameObject.SetActive(false);
            mainWindow.SetActive(false);
            settingsWindow.SetActive(false);
        }
    } 
}
