using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StartMenu : MonoBehaviour
{

    private string sceneKey;
    private void Start()
    {
        //sceneKey = $"{SceneManager.GetActiveScene().name}_FirstTime";
        //
        //if (PlayerPrefs.GetInt(sceneKey, 0) == 0)
        //{
        //   
        //    PlayerPrefs.SetInt(sceneKey, 1);
        //    PlayerPrefs.Save();
        //}
        //else
        //{
        //    Debug.Log("LevelMenu");
        //}
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(sceneKey, 0);
        PlayerPrefs.Save();
    }
    public void Play()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void Levels()
    {
    }
    public void Settings()
    {

    }
    public void Exit()
    {
      #if UNITY_EDITOR
      
              UnityEditor.EditorApplication.isPlaying = false;
              PlayerPrefs.SetInt(sceneKey, 0);
              PlayerPrefs.Save();
      #else
      
              Application.Quit();
              PlayerPrefs.SetInt(sceneKey, 0);
              PlayerPrefs.Save();
      #endif
    }

}
