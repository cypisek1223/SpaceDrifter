using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StartMenu : MonoBehaviour
{
    public Animator SlideMenu;
    public void Play()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void Levels()
    {
        SlideMenu.Play("SlideMenuLeft");
    }
    public void Settings()
    {

    }
    public void Exit()
    {
      #if UNITY_EDITOR
      
              UnityEditor.EditorApplication.isPlaying = false;
      #else
      
              Application.Quit();
      #endif
    }
    public void Return()
    {
        SlideMenu.Play("SlideMenuRight");
    }
}
