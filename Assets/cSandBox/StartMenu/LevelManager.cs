using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
namespace SpaceDrifter2D
{
    public class LevelManager : MonoBehaviour
    {

        [SerializeField] public LevelData level;
        [SerializeField] public PlanetData planetData;
        [SerializeField] public Canvas LevelUI;

        public void OnClick()
        {           
            GameManager.LevelLoad(level, planetData);
            LevelUI.enabled = false;
        }

        public void SelectLevel(LevelData level, PlanetData planetData,Canvas LevelUI)
        {
            GameManager.LevelLoad(level, planetData);
            LevelUI.enabled = false;
        }

        //public Button[] levelButtons;
        //
        //public string levelNamePrefix = "Level_";

        //private void Start()
        //{
        //    for (int i = 0; i < levelButtons.Length; i++)
        //    {
        //        int levelIndex = i + 1;
        //        levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        //    }
        //}
        //
        //private void LoadLevel(int levelIndex)
        //{
        //    string levelName = levelNamePrefix + levelIndex.ToString();
        //
        //    if (Application.CanStreamedLevelBeLoaded(levelName))
        //    {
        //        SceneManager.LoadScene(levelName);
        //    }
        //    else
        //    {
        //        Debug.LogError("Poziom" + levelName + " nie istnieje");
        //    }
        //}
    }
}
