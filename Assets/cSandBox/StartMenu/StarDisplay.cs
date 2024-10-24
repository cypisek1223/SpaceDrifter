using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace SpaceDrifter2D
{
    public class StarDisplay : MonoBehaviour
    {
        public Image[] starImages;
        private ScoreKeeper scoreKeeper;
        public int levelId = 1;


        void Start()
        {
            //DO ZMIANY
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
            UpdateStarDisplay();
        }

        private void UpdateStarDisplay()
        {
            int stars = scoreKeeper.GetStars(levelId);

            for (int i = 0; i < starImages.Length; i++)
            {
                //NIE CZAJE TEGO
                starImages[i].enabled = i < stars;
            }
        }
    }
}