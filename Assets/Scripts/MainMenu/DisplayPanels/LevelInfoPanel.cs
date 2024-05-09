using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class LevelInfoPanel : MonoBehaviour
    {
        [SerializeField] private GameObject lockedPanel;
        [SerializeField] private GameObject unlockedPanel;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI starsText;

        private LevelData level;
        private LevelListPanel levelsDisplay;

        public void Init(LevelData level, LevelListPanel levelsListPanel)
        {
            this.level = level;
            nameText.text = level.Name;
            levelsDisplay = levelsListPanel;

            timeText.text = TimeSpan.FromSeconds(level.FinishTime).ToString("mm\\:ss");
            coinsText.text = level.CollectedCoins.ToString();
            starsText.text = level.CollectedStars.ToString();

            if(level.Locked)
            {
                lockedPanel.SetActive(true);
                unlockedPanel.SetActive(false);
            }
            else
            {
                lockedPanel.SetActive(false);
                unlockedPanel.SetActive(true);
            }
        }

        public void OnClick()
        {
            if (level.Locked) return;

            levelsDisplay.SelectLevel(level);
        }
    }
}