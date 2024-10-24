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
    }
}
