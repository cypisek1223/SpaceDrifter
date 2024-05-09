using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LevelStatistics : MonoBehaviour
    {
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI coinsText;


        public void Display(int seconds, int coins)
        {
            statsPanel.SetActive(true);
            timeText.text = TimeSpan.FromSeconds(seconds).ToString("mm\\:ss");
            coinsText.text = coins.ToString();
        }
    } 
}
