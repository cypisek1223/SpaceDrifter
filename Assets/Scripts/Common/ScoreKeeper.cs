using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private int points;
        [SerializeField] int totalPoints;

        [Header("GAME DATE")]
        public DataLevel dataLevel;
        [SerializeField] List<LevelDate2> previewBoard;
    
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

        #region Unlock Levels
        void UnlockLevel(int countUnlock)
        {
            for(int i=0; i <= countUnlock; i++)
            {
                dataLevel.levels[i].blocked = true;
            }
        }

        #endregion
        #region Stars System

        private void Start()
        {
            for (int i = 0; i < dataLevel.levels.Length; i++)
            {
                //previewBoard[i].levelId = dataLevel.levels[i].levelId;
                previewBoard[i].blocked = dataLevel.levels[i].blocked;
                previewBoard[i].starts = dataLevel.levels[i].starts;
                previewBoard[i].theBestTimeForStars = dataLevel.levels[i].theBestTimeForStars;
                previewBoard[i].bestTime = dataLevel.levels[i].bestTime;
                previewBoard[i].mediumTimeForStars = dataLevel.levels[i].mediumTimeForStars;
                previewBoard[i].lowTimeForStars = dataLevel.levels[i].lowTimeForStars;
            }
        }


        public void SaveLevelCompletionTime(int levelId, float completionTime)
        {


            var level = dataLevel.levels[levelId];
            if (level != null)
            {
                levelStars = CalculateStars(dataLevel, levelId, completionTime);


                if (levelStars > dataLevel.levels[levelId].starts)
                {
                    dataLevel.levels[levelId].starts = levelStars;
                }

                if (dataLevel.levels[levelId].bestTime < completionTime)
                    dataLevel.levels[levelId].bestTime = completionTime;

            }

        }
        private int CalculateStars(DataLevel _dataLevel, int levelId, float completionTime)
        {
            if (completionTime < _dataLevel.levels[levelId].bestTime)
            {
                return 3;
            }
            else if (completionTime <= _dataLevel.levels[levelId].mediumTimeForStars)
            {
                return 2;
            }
            else if (completionTime <= _dataLevel.levels[levelId].lowTimeForStars)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int GetStars(int levelId, DataLevel dataLevel)
        {
            return dataLevel.levels[levelId].starts; ;

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

        
        private IEnumerator AnimationPointsSequence()
        {
            starText.text = "0";
            coinsText.text = "0";
            points = 0;

            yield return StartCoroutine(PointsAnimationTwo(0, coins, coinsTextEndMenu));
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, GetStars(id, dataLevel), starText));
            yield return new WaitForSeconds(0.01f);
            //yield return StartCoroutine(PointsAnimationTwo(0, time, timeEndMenuText));
            yield return StartCoroutine(PointsAnimationTwo(0, misionPoint, misionPointsText));
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, bonusCoin, bonusCoinText));
            yield return new WaitForSeconds(0.01f);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PointsAnimationTwo(0, points, summaryPointsText));
            totalPoints += points;
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

            points += currentValue;

            uiText.text = currentValue.ToString("0");
        }
        #endregion
        
    }
    [System.Serializable]
    public class LevelDate2
    {
        public int levelId;
        public bool blocked;
        public bool finished;
        public bool recommended;
        public int starts;
        
        public float bestTime;
        public float theBestTimeForStars;
        public float mediumTimeForStars;
        public float lowTimeForStars;

        //public Image[] stars;
        //public Button button;
        public LevelData level;
    }


      
}
