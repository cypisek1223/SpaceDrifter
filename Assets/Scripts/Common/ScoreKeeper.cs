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
        [SerializeField] private TextMeshProUGUI coinsTextEndMenu;
        [SerializeField] private TextMeshProUGUI starText;
        [SerializeField] private TextMeshProUGUI timeText;

        private LevelData level;
        
        [SerializeField] private int id = 0;
        [SerializeField] private int levelStars = 0;

        private TimeSpan time;
        private int coins;
        private int prevTime;
        private int prevCoins;

        
       

        //public void Start()
        //{
        //
        //}
        public void HideAll()
        {
            levelStats.gameObject.SetActive(false);
            Hide();
        }
        public void InitLevel(LevelData level)
        {
            levelStats.gameObject.SetActive(false);

            this.level = level;
            //Cyprian ADDED THIS
            id = level.levelID;
            prevTime = level.FinishTime;
            prevCoins = level.CollectedCoins;
            coins = 0;
            coinsText.text = "0";
            starText.text = "0";
            time = new TimeSpan(0);
            timeText.text = time.ToString("mm\\:ss");

            //ADD LEVEL ID THIS 
            
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

            //Cyprian ADDED THIS
           
            if(id != 0 || id != null)
            {
                SaveLevelCompletionTime(id, level.FinishTime);
                starText.text = levelStars.ToString();
                StarCounting(coins);
            }
          
            //

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

        public float GetTime()
        {
            float levelTime = (float)time.TotalSeconds;
            return levelTime;
        }


        //Cyprian ADDED THIS STAR SYSTEM 

        #region Stars System

        [SerializeField]
        public List<LevelDataTime> levels;
        
        //CZEGO AWAKE NIE DZIALA
        private void  Start()
        {
          LoadAllLevelData();
        }
        
        private void LoadAllLevelData()
        {
            foreach (LevelDataTime level in levels)
            {
                level.stars = PlayerPrefs.GetInt($"Level_{level.levelId}__Stars", 0);
            }
        }
        
        public void SaveLevelCompletionTime(int levelId, float completionTime)
        {
            
            //OGARNIJ JAK TO DZIALA
            LevelDataTime level = levels.Find(l => l.levelId == levelId);
            if (level != null)
            {
                Debug.Log("Zapisywanie Levela");
                levelStars = CalculateStars(completionTime, level);


                if (levelStars > PlayerPrefs.GetInt($"Level_{levelId}_Stars", 0))
                {
                    level.stars = levelStars;
                }
                else
                {
                    level.stars = GetStars(levelId);
                }
                    
                PlayerPrefs.SetInt($"Level_{levelId}_Stars", level.stars);
                PlayerPrefs.SetFloat($"Level_{levelId}_CompletionTime", completionTime);

                if (level.bestTime < completionTime)
                    level.bestTime = completionTime;
                    PlayerPrefs.SetFloat($"Level_{levelId}_TheBestTime", level.bestTime);

                PlayerPrefs.Save();
            }
            
        }
        
        private int CalculateStars(float completionTime, LevelDataTime level)
        {
            if (completionTime < level.mediumTime)
            {
                Debug.Log("Return 3 ," + level.mediumTime+" czas na 3 gwiazdki:"+ completionTime);
                return 3;      
            }
            else if (completionTime <= level.lowTime)
            {
                Debug.Log("Return 2 ," + level.lowTime + " czas na 2 gwiazdki:" + completionTime);
                return 2;
            }
            else
            {
                Debug.Log("Return 1 ," + level.mediumTime + " czas na 1 gwiazdki:" + completionTime);
                return 1;
            }
        
        }
        
        public int GetStars(int levelId)
        {
            return PlayerPrefs.GetInt($"Level_{levelId}_Stars", 0);
        }

        #endregion


        #region Points Animation


        private int targetPoints;
        
        private int currentPoint;
        private float animationDuration = 1.5f;


        public void StarCounting(int finalPoints)
        {
            targetPoints = coins;
            StartCoroutine(CountPointsAnimation());
        }

        private IEnumerator CountPointsAnimation()
        {
            float elapsedTime = 0f;

            while(elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                currentPoint = (int)Mathf.Lerp(0, targetPoints, elapsedTime / animationDuration);
                coinsTextEndMenu.text = currentPoint.ToString("0");
                yield return null;
            }
            currentPoint = targetPoints;
            coinsTextEndMenu.text = currentPoint.ToString("0");
        }
        #endregion
    }



    [System.Serializable]
    public class LevelDataTime
    {
        public int levelId;
        public float mediumTime;
        public float lowTime;
        public float bestTime;
        public int stars;
    }

   


}