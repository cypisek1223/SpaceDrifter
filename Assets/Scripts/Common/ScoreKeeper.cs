using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class ScoreKeeper : Singleton<ScoreKeeper>
    {
        [SerializeField] private GameObject displayPanel;
        [SerializeField] private LevelStatistics levelStats;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI timeText;

        private LevelData level;

        private TimeSpan time;
        private int coins;
        private int prevTime;
        private int prevCoins;


        public void HideAll()
        {
            levelStats.gameObject.SetActive(false);
            Hide();
        }
        public void InitLevel(LevelData level)
        {
            levelStats.gameObject.SetActive(false);

            this.level = level;
            prevTime = level.FinishTime;
            prevCoins = level.CollectedCoins;
            coins = 0;
            coinsText.text = "0";
            time = new TimeSpan(0);
            timeText.text = time.ToString("mm\\:ss");
        }

        public void StartLevel()
        {
            Show();
            StartCoroutine(CountTime());
        }

        public void PauseTime()
        {
            StopAllCoroutines();
        }

        public void ResumeTime()
        {
            StartCoroutine(CountTime());
        }

        public void CoinCollect()
        {
            coins++;
            coinsText.text = coins.ToString();
        }

        public void Gate()
        {
            //Reduce time or smth...
        }

        public void FinishCurrentLevel()
        {
            StopAllCoroutines();

            if (prevTime == 0 || time.TotalSeconds < prevTime)
            {
                level.FinishTime = (int)time.TotalSeconds;
            }

            if (coins > prevCoins)
            {
                level.CollectedCoins = coins;
            }

            Hide();
            levelStats.gameObject.SetActive(true);
            levelStats.Display((int)time.TotalSeconds, coins);
        }

        private IEnumerator CountTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                time = time.Add(TimeSpan.FromSeconds(1));
                timeText.text = time.ToString("mm\\:ss");
            }
        }

        private void Show()
        {
            displayPanel.SetActive(true);
        }
        private void Hide()
        {
            displayPanel.SetActive(false);
        }
    }
}