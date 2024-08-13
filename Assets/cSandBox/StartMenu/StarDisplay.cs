using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class StarDisplay : MonoBehaviour
{
    public Image[] starImages;
    private LevelTimeManager levelTimeManager;
    public int levelId = 1;


    void Start()
    {
        //DO ZMIANY
        levelTimeManager = FindObjectOfType<LevelTimeManager>();
        UpdateStarDisplay();
    }

    private void UpdateStarDisplay()
    {
        int stars = levelTimeManager.GetStars(levelId);

        for(int i =0; i < starImages.Length; i++)
        {
            //NIE CZAJE TEGO
            starImages[i].enabled = i < stars;
        }
    }
}
