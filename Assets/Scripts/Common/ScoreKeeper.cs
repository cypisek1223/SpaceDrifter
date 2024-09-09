using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class ScoreKeeper : Singleton<ScoreKeeper>
    {
 
        [Header("Points Animation")]
        [SerializeField] private float animationDuration = 1.5f;

        [SerializeField] private GameObject displayPanel;
        [SerializeField] private LevelStatistics levelStats;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI coinsTextEndMenu;
        [SerializeField] private TextMeshProUGUI starText;
        [SerializeField] public TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI timeEndMenuText;
        [SerializeField] private TextMeshProUGUI misionPointsText;
        [SerializeField] private TextMeshProUGUI bonusCoinText;

        [SerializeField] private TextMeshProUGUI summaryPointsText;
        [SerializeField] private int currentSummaryPoints;


        [Header("Level Database")]
        private LevelData level;

        [SerializeField] private int id = 0;
        [SerializeField] private int levelStars = 0;

        private TimeSpan time;
        private int coins;
        private int bonusCoin;
        private int misionPoint;
        private int prevTime;
        private int prevCoins;


        public int playerPoints;
        public int totalPoints;


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
            bonusCoin = 0;
            misionPoint = 0;
            currentSummaryPoints = 0;
            
            starText.text = "0";
            coinsText.text = "0";
            coinsTextEndMenu.text = "0";
            bonusCoinText.text = "0";
            misionPointsText.text = "0";
            summaryPointsText.text = "0";           
            timeEndMenuText.text = "0";

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
        public void BonusCoinCollect()
        {
            bonusCoin++;
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

            if (id != 0)
            {
                SaveLevelCompletionTime(id, level.FinishTime);
                //starText.text = levelStars.ToString();
                StarCounting();
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
                yield return new WaitForSeconds(2);
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
        private void Start()
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
                return 3;
            }
            else if (completionTime <= level.lowTime)
            {
                return 2;
            }
            else
            {             
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
        

       

        public void StarCounting()//int finalPoints)
        {
            //targetPoints = coins;
            //StartCoroutine(PointsAnimation());
            StartCoroutine(AnimationPointsSequence());
            
        }

        //private IEnumerator PointsAnimation()
        //{
        //    float elapsedTime = 0f;
        //
        //    while(elapsedTime < animationDuration)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        currentPoint = (int)Mathf.Lerp(0, targetPoints, elapsedTime / animationDuration);
        //        coinsTextEndMenu.text = currentPoint.ToString("0");
        //        yield return null;
        //    }
        //    currentPoint = targetPoints;
        //    coinsTextEndMenu.text = currentPoint.ToString("0");
        //}

        
        private IEnumerator AnimationPointsSequence()
        {
            starText.text = "0";
            coinsText.text = "0";

            yield return StartCoroutine(PointsAnimationTwo(0, coins, coinsTextEndMenu));
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, GetStars(id), starText));
            yield return new WaitForSeconds(0.01f);
            //yield return StartCoroutine(PointsAnimationTwo(0, time, timeEndMenuText));
            yield return StartCoroutine(PointsAnimationTwo(0, misionPoint, misionPointsText));
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, bonusCoin, bonusCoinText));
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, totalPoints, summaryPointsText));
        }
        private IEnumerator PointsAnimationTwo( int currentValue, int targetValue, TextMeshProUGUI uiText)
        {
            float elapsedTime = 0f;
        
            while (elapsedTime < animationDuration && targetValue > 0)
            {
                elapsedTime += Time.deltaTime*5;
                currentValue = (int)Mathf.Lerp(0, targetValue, elapsedTime / animationDuration);
                uiText.text = currentValue.ToString("0");
                yield return null;
            }
            currentValue = targetValue;

            totalPoints += currentValue;

            uiText.text = currentValue.ToString("0");
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